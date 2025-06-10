namespace Dreslay.DTO
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TailorId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
