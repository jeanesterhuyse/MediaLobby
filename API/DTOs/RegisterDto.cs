using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string userEmail { get; set; }
        
        [Required]
        public string userpassword { get; set; }
    }
}