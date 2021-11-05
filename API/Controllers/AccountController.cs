using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext context;
        private readonly ITokenService tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await UserExist(registerDto.Useremail)) return BadRequest("UserEmail is taken");


            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username,
                UserEmail = registerDto.Useremail.ToLower(),
                UserPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Userpassword)),
                PasswordSalt = hmac.Key
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return new UserDto
            {
                Useremail = user.UserEmail,
                Token = this.tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await this.context.Users
                .SingleOrDefaultAsync(x => x.UserEmail == loginDto.Useremail);

            if (user == null) return Unauthorized("Invalid userEmail");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.UserPasswordHash[i]) return Unauthorized("Invalid password");
            }
            
            return new UserDto
            {
                Useremail = user.UserEmail,
                Token = this.tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string useremail)
        {
            return await this.context.Users.AnyAsync(x => x.UserEmail == useremail.ToLower());
        }


    }
}