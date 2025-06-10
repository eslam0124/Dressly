namespace Dreslay.DTO
{
    public class ComplaintDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TailorId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
