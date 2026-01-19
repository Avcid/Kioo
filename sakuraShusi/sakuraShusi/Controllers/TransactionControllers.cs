using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using sakuraShusi.DTO;
using sakuraShusi.Models;

namespace sakuraShusi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController(SakuraSushiContext _context) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Cashier,Waiter")]
        public IActionResult CreateTransaction([FromForm] string tableNumber)
        {
            var table = _context.Tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
                return NotFound(new { message = "Table not found" });

            var hasOpenTransaction = _context.Transactions
                .Any(t => t.TableId == table.Id && t.ClosedAt == null);

            if (hasOpenTransaction)
                return BadRequest(new { message = "Table already has an open transaction" });

            var uniqueCode = GenerateUniqueCode();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                TableId = table.Id,
                UniqueCode = uniqueCode,
                OpenedAt = DateTimeOffset.UtcNow,
                TotalAmount = 0m
            };

            var username = User.Identity?.Name;
            var cashier = _context.Users.FirstOrDefault(u => u.Username == username);

            if (cashier != null && cashier.Role == "Cashier")
                transaction.CashierId = cashier.Id;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok(new { uniqueCode });
        }

        [HttpGet("{uniqueCode}/Orders")]
        public IActionResult GetOrders(string uniqueCode)
        {
            var transaction = _context.Transactions
                .Include(t => t.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Item)
                .FirstOrDefault(t => t.UniqueCode == uniqueCode);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var orders = transaction.Orders
                .OrderBy(o => o.OrderedAt)
                .Select(o => new
                {
                    o.Id,
                    o.OrderedAt,
                    o.Amount,
                    Items = o.OrderItems.Select(oi => new
                    {
                        oi.Id,
                        oi.ItemId,
                        ItemName = oi.Item.Name,
                        oi.Quantity,
                        oi.Price,
                        oi.Status
                    })
                });

            return Ok(orders);
        }

        [HttpPost("{uniqueCode}/Pay")]
        [Authorize(Roles = "Cashier")]
        public IActionResult PayTransaction(string uniqueCode)
        {
            var transaction = _context.Transactions
                .Include(t => t.CartItems)
                .FirstOrDefault(t => t.UniqueCode == uniqueCode && t.ClosedAt == null);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            if (transaction.ClosedAt != null)
                return BadRequest(new { message = "Transaction is already paid" });

            if (!transaction.CartItems.Any())
                return BadRequest(new { message = "Cart is empty" });

            transaction.TotalAmount = transaction.CartItems.Sum(ci => ci.TotalPrice);

            transaction.ClosedAt = DateTimeOffset.UtcNow;

            var username = User.Identity?.Name;
            var cashier = _context.Users.FirstOrDefault(u => u.Username == username);

            if (cashier != null)
                transaction.CashierId = cashier.Id;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Transaction paid successfully",
                totalAmount = transaction.TotalAmount
            });
        }


        [HttpPut("{uniqueCode}/Orders/{orderId:guid}/Items/{itemId:guid}/Status")]
        [Authorize(Roles = "Chef,Waiter")]
        public IActionResult UpdateOrderItemStatus(string uniqueCode, Guid orderId, Guid itemId, [FromBody] UpdateOrderItemStatusDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Status))
                return UnprocessableEntity(new { message = "Status is required" });

            var transaction = _context.Transactions.FirstOrDefault(t => t.UniqueCode == uniqueCode);
            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == orderId && o.TransactionId == transaction.Id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ItemId == itemId);
            if (orderItem == null)
                return NotFound(new { message = "Item not found" });

            orderItem.Status = dto.Status;
            _context.SaveChanges();

            return Ok(new { message = "Item status updated successfully" });
        }

        private string GenerateUniqueCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            while (true)
            {
                var code = new string(Enumerable.Repeat(chars, 4)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                var exists = _context.Transactions.Any(t => t.UniqueCode == code && t.ClosedAt == null);

                if (!exists)
                    return code;
            }
        }
    }
}
