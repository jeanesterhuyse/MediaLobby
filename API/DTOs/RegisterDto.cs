using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string UserEmail { get; set; }
        
        [Required]
        public string Userpassword { get; set; }
    }
}