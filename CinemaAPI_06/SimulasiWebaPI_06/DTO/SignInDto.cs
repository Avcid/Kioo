using SimulasiWebaPI_06.Models;

namespace SimulasiWebaPI_06.DTO
{
    public class SignInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class GenreListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class schaduleDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public double Price { get; set; }
        public virtual Theater Theater { get; set; } = null!;

        public List<MovieDto> Movie { get; set; } = new();
    }

    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Duration { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string Poster { get; set; } = null!;
    }
}
