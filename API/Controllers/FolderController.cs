using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FolderController
    {
        private readonly IFolderRepository folderRepository;
        private readonly IMapper mapper;
        private readonly DataContext context;
        
       
        public FolderController(IFolderRepository folderRepository, IMapper mapper,DataContext context)
        {
            this.context= context;
            this.mapper = mapper;
            this.folderRepository = folderRepository;
        }
        
         [HttpPost("create-folder")]
        public async Task<ActionResult<NewFolderDto>> CreateFolderAsync(FolderDto folderDto)
        {
            var folders=this.mapper.Map<Folders>(folderDto);
            this.context.Folders.Add(folders);
            await this.context.SaveChangesAsync();
            return new NewFolderDto
            {
                folderName= folders.folderName,
                appUserId=folders.appUserId
            };
        }
    }
}