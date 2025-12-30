using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SimulasiAkhir.DTOs;
using SimulasiAkhir.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimulasiAkhir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly EsemkaOnePlusContext _db;
        public CustomerController(EsemkaOnePlusContext db) => _db = db;

        [HttpGet("Customer")]
        public async Task<IActionResult> getCustomer()
        {
            var data = await _db.Customers
                .OrderBy(g => g.Id)
                .Select(g => new CustomerDTO { Id = g.Id,
                    Email = g.Email,
                    Name = g.Name,
                    Password = g.Password,
                    Gender = g.Gender,
                    DateOfBirth = g.DateOfBirth,
                    PhoneNumber = g.PhoneNumber,
                    Address = g.Address,
                    Role = g.Role,
                    LoyaltyId = g.LoyaltyId,
                    LoyaltyExpiredDate = g.LoyaltyExpiredDate,
                    PhotoPath = g.PhotoPath,
                    TotalPoint = g.TotalPoint,
                    CreatedAt = g.CreatedAt,
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost("Customer")]
        public IActionResult NewCustomer(CustomerDTO dto)
        {
            var id = dto.Id;
            var name = dto.Name;
            var email = dto.Email;
            var pass = dto.Password;
            var gender = dto.Gender;
            var dateOfbirth = dto.DateOfBirth;
            var phone = dto.PhoneNumber;
            var address = dto.Address;
            var role = dto.Role;
            var royaliti2 = dto.LoyaltyId;
            var loyalty = dto.LoyaltyExpiredDate;
            var photo = dto.PhotoPath;
            var tpoint = dto.TotalPoint;
            var creatAt = dto.CreatedAt;

            if (id == null || name == null || email == null || pass == null || gender == null || dateOfbirth == null
                || phone == null || address == null || role == null || loyalty == null || royaliti2 == null || photo == null
                || tpoint == null || creatAt == null)
            {
                BadRequest(new
                {
                    Message = "Valeu can't be empty"
                });
            }

            Customer  newCustomer = new Customer
            {
                Id = id,
                Name = name,
                Email = email,
                Password = pass,
                Gender = gender,
                DateOfBirth = dateOfbirth,
                PhoneNumber = phone,
                Address = address,
                Role = role,
                LoyaltyExpiredDate = loyalty,
                LoyaltyId = royaliti2,
                PhotoPath = photo,
                TotalPoint = tpoint,
                CreatedAt = creatAt,
            };

            _db.Customers.Add(newCustomer);
            _db.SaveChanges();

            return Ok(new
            {
                Message = "Successfuly add customer",
                Data = new
                {
                    Customer = newCustomer.Id,
                    FirstName = name,
                    Email = email,
                    Password = pass,
                    Gender = gender,
                    DateOfBirth = dateOfbirth,
                    PhoneNumber = phone,
                    Address = address,
                    Role = role,
                    LoyaltyExpiredDate = loyalty,
                    PhotoPath = photo,
                    TotalPoint = tpoint,
                    CreatedAt = creatAt,
                }
            });
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerId(int id)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var customer = await _db.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
                return NotFound(new { message = "Movie Not Found" });

            var genres = await _db.Customers
                .Where(mg => mg.Id == id)
                .Join(_db.Customers, mg => mg.Id, g => g.Id, (mg, g) => g.Name)
                .ToListAsync();

            return Ok(new CustomerDTO
            {
                Id = id,
                Email = customer.Email,
                Name = customer.Name,
                Password = customer.Password,
                Gender = customer.Gender,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                Role = customer.Role,
                LoyaltyId = customer.LoyaltyId,
                LoyaltyExpiredDate = customer.LoyaltyExpiredDate,
                PhotoPath = customer.PhotoPath,
                TotalPoint = customer.TotalPoint,
                CreatedAt = customer.CreatedAt,
            });
        }
        [HttpPut("{id:int}")]
        public IActionResult PutCustomer(CustomerDTO pcustomer)
        {
            return Ok(pcustomer);
        }
    }
}
