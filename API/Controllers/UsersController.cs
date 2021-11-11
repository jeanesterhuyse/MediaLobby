using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IPhotoService photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            this.photoService = photoService;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await this.userRepository.GetMembersAsync();
            return Ok(users);
        }


        [HttpGet("{UserEmail}", Name  = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string UserEmail)
        {
            return await this.userRepository.GetMemberAsync(UserEmail);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {

            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            this.mapper.Map(memberUpdateDto, user);
            this.userRepository.Update(user);
            if (await this.userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Could not update user");
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var result = await this.photoService.AddPhotoAsync(file);
            if(result.Error !=null) return BadRequest(result.Error.Message);
            var photo= new Photo{
                url = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId
            };
            if(user.photos.Count==0){
                photo.isMain=true;
            }
            user.photos.Add(photo);
            if(await this.userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new{userEmail = user.userEmail}, this.mapper.Map<PhotoDto>(photo));
                
            }
                
            return BadRequest("Error with adding photo");
            
        } 
         [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());

            var photo = user.photos.FirstOrDefault(x => x.id == photoId);

            if (photo.isMain) return BadRequest("This picture is already your profile picture");

            var currentMain = user.photos.FirstOrDefault(x => x.isMain);
            if (currentMain != null) currentMain.isMain = false;
            photo.isMain = true;

            if (await this.userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Could not set it as profile picture");
        }

    }
}