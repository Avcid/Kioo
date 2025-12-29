using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulasiWebaPI_06.DTO;
using SimulasiWebaPI_06.Models;

namespace SimulasiWebaPI_06.Controllers
{

    [Route("api/cinemaflix/v1/Schadule")]
    [ApiController]
    public class SheduleController : ControllerBase
    {
        private readonly CinemaApiContext _db;
        public SheduleController(CinemaApiContext db) => _db = db;

        [HttpGet("Schedule")]
        public async Task<IActionResult> GetSchadule()
        {
            var data = await _db.Genres
            .OrderBy(g => g.Id)
            .Select(g => new schaduleDto { Id = g.Id,})
            .ToListAsync();

            return Ok(data);
        }
    }
}
