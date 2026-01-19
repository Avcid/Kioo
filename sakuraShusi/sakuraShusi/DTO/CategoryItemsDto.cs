namespace sakuraShusi.DTO
{
    public class CategoryItemsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
            
        public string Description { get; set; } = null!;

        public List<ItemDTO> Items { get; set; } = new();
    }
}
