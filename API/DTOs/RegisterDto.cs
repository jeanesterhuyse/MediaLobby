using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string userEmail { get; set; }
        
        [Required]
        public string userPassword { get; set; }
    }
}