namespace Dreslay.DTO
{
    public class FabricDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal Price { get; set; } 
        public string ImageUrl { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
