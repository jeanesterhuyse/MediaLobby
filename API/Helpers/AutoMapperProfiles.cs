
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;
namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest=>dest.photoUrl,opt=>opt.MapFrom(src=>src.photos
                .FirstOrDefault(x=>x.isMain).url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto,AppUser>();
            CreateMap<RegisterDto,AppUser>();
        }
    }
}