using System.ComponentModel.DataAnnotations;

namespace Dreslay.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string User_Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ?Address { get; set; }
        public string Phone { get; set; } = null!;
    }
}
