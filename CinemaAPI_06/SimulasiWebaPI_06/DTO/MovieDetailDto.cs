namespace SimulasiWebaPI_06.DTO
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Poster { get; set; } = null!;
        public DateTime Created { get; set; }

        public List<GenreListDto> Genres { get; set; } = new ();

    }
}
