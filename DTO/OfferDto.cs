namespace Dreslay.DTO
{
    public class OfferDto
    {
        public int Id { get; set; }
        public int TailorId { get; set; }
        public int FabricId { get; set; }
        public decimal Price { get; set; }
        public string? Note { get; set; }
    }
}
