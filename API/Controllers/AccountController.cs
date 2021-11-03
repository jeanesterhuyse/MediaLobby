using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext context;
        public AccountController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {

            if(await UserExist(registerDto.Useremail)) return BadRequest("UserEmail is taken");


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

            return user;
        }

        private async Task<bool> UserExist(string useremail)
        {
            return await this.context.Users.AnyAsync(x => x.UserEmail == useremail.ToLower());
        }
        

    }
}