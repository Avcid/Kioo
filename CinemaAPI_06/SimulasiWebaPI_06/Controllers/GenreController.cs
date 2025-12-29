using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulasiWebaPI_06.DTO;
using SimulasiWebaPI_06.Models;

[ApiController]
[Route("api/    genre")]
public class GenreController : ControllerBase
{
    private readonly CinemaApiContext _db;
    public GenreController(CinemaApiContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var data = await _db.Genres
            .OrderBy(g => g.Id)
            .Select(g => new GenreListDto { Id = g.Id, })
            .ToListAsync();

        return Ok(data);
    }
}
