using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
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

            if (await UserExist(registerDto.userEmail)) return BadRequest("UserEmail is taken");


            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                userName = registerDto.username,
                userEmail = registerDto.userEmail.ToLower(),
                userPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.userpassword)),
                passwordSalt = hmac.Key
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return new UserDto
            {
                userEmail = user.userEmail,
                token = this.tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await this.context.Users
            .Include(p => p.photos)
            .SingleOrDefaultAsync(x => x.userEmail == loginDto.userEmail.ToLower());

            if (user == null) return Unauthorized("Invalid userEmail");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.userPasswordHash[i]) return Unauthorized("Invalid password");
            }
            
            return new UserDto
            {
                userEmail = user.userEmail,
                token = this.tokenService.CreateToken(user),
                photoUrl = user.photos.FirstOrDefault(x=>x.isMain)?.url
            };
        }

        private async Task<bool> UserExist(string userEmail)
        {
            return await this.context.Users.AnyAsync(x => x.userEmail == userEmail.ToLower());
        }


    }
}