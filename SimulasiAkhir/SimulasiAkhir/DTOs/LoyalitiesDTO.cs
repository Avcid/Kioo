namespace SimulasiAkhir.DTOs
{
    public class LoyalitiesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal RequiredPoint { get; set; }

        public int Multiplier { get; set; }
    }
}
