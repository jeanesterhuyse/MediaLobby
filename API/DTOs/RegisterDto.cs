namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        public string Useremail { get; set; }

        public string Userpassword { get; set; }
    }
}