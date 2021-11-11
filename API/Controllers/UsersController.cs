using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users =await this.userRepository.GetMembersAsync();
            return Ok(users);
        }


        [HttpGet("{UserEmail}")]
        public async Task<ActionResult<MemberDto>> GetUser(string UserEmail)
        {
            return await this.userRepository.GetMemberAsync(UserEmail);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await this.userRepository.GetUserByUserEmailAsync(userEmail);
            this.mapper.Map(memberUpdateDto,user);
            this.userRepository.Update(user);
            if (await this.userRepository.SaveAllAsync()) return NoContent();
            
            return BadRequest("Could not update user");
        }

    }
}