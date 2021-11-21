using System;

using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IPhotoService photoService;
        private int newFolderId;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService, DataContext context)
        {
            this.photoService = photoService;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.context = context;
        }
        [HttpGet]
        public async Task<MemberDto[]> GetUsers()
        {
            var users = await this.userRepository.GetMembersAsync();
            return this.mapper.Map<MemberDto[]>(users);
        }

        [HttpGet("download/{url}")]
        public async Task<ActionResult> download(String url){
            WebClient client = new WebClient();
            client.DownloadFileAsync(new Uri(url),@"c:\image.jpg");        
            return Ok();
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

        [HttpPut("update-folder/{folderId}/{name}")]
        public async Task<ActionResult> UpdateFolder(int folderId, string name)
        {
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var folder = user.folders.FirstOrDefault(x => x.id == folderId);
            folder.folderName=name;
            await this.userRepository.SaveAllAsync();
           return Ok();
        }

        [HttpGet("getlast")]
        public async Task<int> GetLast(){
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var last=user.photos
                       .OrderByDescending(p => p.id)
                       .FirstOrDefault();
                return last.id;
        }

         [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var result = await this.photoService.AddPhotoAsync(file);
            await CreateFolderAsync("Unassigned photo");
            if(result.Error !=null) return BadRequest(result.Error.Message);
            var photo= new Photo{
                url = result.SecureUrl.AbsoluteUri,
                publicId = result.PublicId,
                foldersId=this.newFolderId
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

        [HttpPut("set-profile-photo/{photoId}")]
        public async Task<ActionResult> SetProfilePhoto(int photoId)
        {
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());

            var photo = user.photos.FirstOrDefault(x => x.id == photoId);


            var currentProfile = user.photos.FirstOrDefault(x => x.isMain);
            if (currentProfile != null) currentProfile.isMain = false;
            photo.isMain = true;

            if (await this.userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Could not set it as profile picture");
        }

        [HttpPut("set-foldersId/{photoId}/{foldersId}")]
        public async Task<ActionResult> SetFoldersId(int photoId, int foldersId)
        {
            Console.WriteLine("fddsf");
            var user = await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var photo = user.photos.FirstOrDefault(x => x.id == photoId);
            photo.foldersId =foldersId;
            await this.userRepository.SaveAllAsync();
            return NoContent();

        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId){
            var user=await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var photo=user.photos.FirstOrDefault(x=>x.id==photoId);
            if(photo==null) return NotFound();
            if(photo.isMain) return BadRequest("You cant delete your profile picture");
            if(photo.publicId != null) {
                var result=await this.photoService.DeletePhotoAsync(photo.publicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }
            user.photos.Remove(photo);
            if(await this.userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Could not delete the photo");
        }

        [HttpDelete("delete-folder/{folderId}")]
        public async Task<ActionResult> DeleteFolder(int folderId){
            var user=await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var folder=user.folders.FirstOrDefault(x=>x.id==folderId);
            foreach(var photo in folder.photos){
            await DeletePhoto(photo.id);
            }
             user.folders.Remove(folder);
            await this.userRepository.SaveAllAsync();
            return Ok();

        }

        [HttpPost("create-folder/{folderNameParam}")]
        public async Task<ActionResult<Folders>> CreateFolderAsync(string folderNameParam)
        {
            Console.WriteLine(folderNameParam);
            var user=await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var folder= new Folders{
                folderName=folderNameParam,
                appUserId=user.id
                
            };
            this.context.Folders.Add(folder);
            await this.context.SaveChangesAsync();
            await this.userRepository.SaveAllAsync();
            this.newFolderId=folder.id;
            return folder;
        }
        [HttpPost("create-metadata/{mlocation}/{mtags}/{mdate}/{mcapturedBy}/{mphotoid}")]
        public async Task<ActionResult<MetaData>> CreateMetaDataAsync(string mlocation,string mtags, string mdate,string mcapturedBy,int mphotoid)
        {

            var user=await this.userRepository.GetUserByUserEmailAsync(User.GetUserEmail());
            var newmetaData= new MetaData{
               location=mlocation,
               tags=mtags,
               date=mdate,
               capturedBy=mcapturedBy,
               photoid=mphotoid

            };
            this.context.MetaData.Add(newmetaData);
            await this.context.SaveChangesAsync();
            await this.userRepository.SaveAllAsync();
            return newmetaData;
        }
    }

    
}