using System.ComponentModel.DataAnnotations;

namespace MechanicalInventory.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? ContactNumber { get; set; }

        public string? Password { get; set; }

        public bool Status { get; set; }

        public string? Role { get; set; }
    }
}
