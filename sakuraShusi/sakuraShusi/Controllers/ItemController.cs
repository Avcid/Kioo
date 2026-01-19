using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sakuraShusi.DTO;
using sakuraShusi.Models;

namespace sakuraShusi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(SakuraSushiContext _context) : ControllerBase
    {
        [HttpGet]
        public ActionResult GetItem([FromQuery] string? search)
        {
            var categoriesQuery = _context.Categories
                .Include(c => c.Items)
                .OrderBy(c => c.Name);

            var categories = categoriesQuery.ToList();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();

                foreach (var c in categories)
                {
                    c.Items = c.Items.Where(i => i.Name.ToLower().Contains(s) ||
                    i.Description.ToLower().Contains(s)).OrderBy(i => i.Name).ToList();
                }

                categories = categories.Where(c => c.Items.Any())
                    .ToList();
            }
            else
            {
                foreach (var c in categories)
                {
                    c.Items = c.Items
                        .OrderBy(i => i.Name).ToList();
                }
            }

            var result = categories
                .Select(c => new CategoryItemsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Items = c.Items.Select(i => new ItemDTO
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        Price = i.Price,
                        ImageUrl = i.ImageUrl
                    }).ToList()
                })
                .ToList();

            return Ok(result);
        }
    }
}
