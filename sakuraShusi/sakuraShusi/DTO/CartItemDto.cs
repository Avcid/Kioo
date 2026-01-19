namespace sakuraShusi.DTO
{
    public class CartItemDto
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset AddedAt { get; set; }
    }
}
