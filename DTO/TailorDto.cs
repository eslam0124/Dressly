using System.ComponentModel.DataAnnotations;

namespace Dreslay.DTO
{
    public class TailorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string User_Name { get; set; } = null!;
        public string Password { get; set; }= null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ?Address { get; set; }
        public double AvgRating { get; set; }


    }
}
