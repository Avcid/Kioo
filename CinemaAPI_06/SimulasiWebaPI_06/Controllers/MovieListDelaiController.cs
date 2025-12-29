using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulasiWebaPI_06.DTO;
using SimulasiWebaPI_06.Models;

[ApiController]
public class MovieController : ControllerBase
{
    private readonly CinemaApiContext _db;
    public MovieController(CinemaApiContext db) => _db = db;

    [HttpGet]
    [Route("api/cinemaflix/v1/movie")]
    public async Task<IActionResult> GetMovies([FromQuery] int? genreId)
    {
        if (genreId.HasValue)
        {
            var genreExists = await _db.Genres.AnyAsync(g => g.Id == genreId.Value);
            if (!genreExists)
                return NotFound(new { message = $"Movie with genreid {genreId.Value} not found" });
        }

        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var query = _db.Movies.AsQueryable();

        var data = await query
            .Select(m => new
            {
                m.Id,
                m.Title,
                m.Description,
                m.Poster,
                Genres = _db.MovieGenres
                    .Where(mg => mg.MovieId == m.Id)
                    .Join(_db.Genres, mg => mg.GenreId, g => g.Id, (mg, g) => new { g.Id, g.Name })
            })
            .ToListAsync();

        if (genreId.HasValue)
        {
            data = data
                .Where(x => x.Genres.Any(g => g.Id == genreId.Value))
                .ToList();

            if (data.Count == 0)
                return NotFound(new { message = $"Movie with genreid {genreId.Value} not found" });
        }

        var result = data.Select(x => new MovieDetailDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Poster = $"{baseUrl}/Photos/{x.Poster}",
        });

        return Ok(result);
    }

    [HttpGet]
    [Route("api/cinemaflix/v1/movies/{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
            return NotFound(new { message = "Movie Not Found" });

        var genres = await _db.MovieGenres
            .Where(mg => mg.MovieId == id)
            .Join(_db.Genres, mg => mg.GenreId, g => g.Id, (mg, g) => g.Name)
            .ToListAsync();

        return Ok(new MovieDetailDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Poster = $"{baseUrl}/Photos/{movie.Poster}",
        });
    }
}
