using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using sakuraShusi.DTO;
using sakuraShusi.Models;

namespace sakuraShusi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController(SakuraSushiContext _context) : ControllerBase
    {
        [HttpGet("{uniqueCode}/Cart")]
        public IActionResult GetCart(string uniqueCode)
        {
            var transaction = _context.Transactions
                .Include(t => t.CartItems)
                    .ThenInclude(ci => ci.Item)
                .FirstOrDefault(t => t.UniqueCode == uniqueCode && t.ClosedAt == null);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var cartItems = transaction.CartItems
                .OrderBy(ci => ci.AddedAt)
                .Select(ci => new CartItemDto
                {
                    ItemId = ci.ItemId,
                    ItemName = ci.Item.Name,
                    ItemDescription = ci.Item.Description,
                    Quantity = ci.Quantity,
                    Price = ci.Price,
                    TotalPrice = ci.TotalPrice,
                    AddedAt = ci.AddedAt
                })
                .ToList();

            return Ok(cartItems);
        }

        [HttpPost("{uniqueCode}/Cart")]
        public IActionResult AddCartItem(string uniqueCode, [FromBody] SimpleCartDto dto)
        {
            if (dto.Quantity <= 0)
                return UnprocessableEntity(new { message = "Quantity must be greater than zero." });

            var transaction = _context.Transactions
                .FirstOrDefault(t => t.UniqueCode == uniqueCode && t.ClosedAt == null);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var item = _context.Items.FirstOrDefault(i => i.Id == dto.ItemId && i.Available);
            if (item == null)
                return NotFound(new { message = "Item not found" });

            var cartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                TransactionId = transaction.Id,
                ItemId = item.Id,
                Quantity = dto.Quantity,
                Price = item.Price,
                TotalPrice = item.Price * dto.Quantity,
                AddedAt = DateTimeOffset.UtcNow
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCart), new { uniqueCode }, new
            {
                cartItem.Id,
                cartItem.ItemId,
                cartItem.Quantity,
                cartItem.Price,
                cartItem.TotalPrice,
                cartItem.AddedAt
            });
        }

        [HttpDelete("{uniqueCode}/Cart/{itemId:guid}")]
        public IActionResult RemoveCartItem(string uniqueCode, Guid itemId)
        {
            var transaction = _context.Transactions
                .Include(t => t.CartItems)
                .FirstOrDefault(t => t.UniqueCode == uniqueCode && t.ClosedAt == null);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var cartItem = transaction.CartItems
                .FirstOrDefault(ci => ci.ItemId == itemId);

            if (cartItem == null)
                return NotFound(new { message = "Item not found in cart" });

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{uniqueCode}/Cart/Order")]
        public IActionResult AddItemToCartOrder(string uniqueCode, [FromBody] AddCartItemDto dto)
        {
            if (dto.Quantity <= 0)
                return UnprocessableEntity(new { message = "Quantity must be greater than zero." });

            var transaction = _context.Transactions
                .FirstOrDefault(t => t.UniqueCode == uniqueCode && t.ClosedAt == null);

            if (transaction == null)
                return NotFound(new { message = "Transaction not found" });

            var item = _context.Items.FirstOrDefault(i => i.Id == dto.ItemId && i.Available);
            if (item == null)
                return NotFound(new { message = "Item not found" });

            var amount = item.Price * dto.Quantity;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                TransactionId = transaction.Id,
                OrderedAt = DateTimeOffset.UtcNow,
                Amount = amount
            };

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ItemId = item.Id,
                Quantity = dto.Quantity,
                Price = item.Price,
                Status = "Pending" 
            };

            order.OrderItems = new List<OrderItem> { orderItem };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Order created successfully",
                order.Id,
                order.OrderedAt,
                order.Amount,
                Items = order.OrderItems.Select(oi => new
                {
                    oi.Id,
                    oi.ItemId,
                    ItemName = item.Name,
                    oi.Quantity,
                    oi.Price,
                    oi.Status
                })
            });
        }

    }
}
