using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int id { get; set; }

        public string userName  { get; set; }
        public string userEmail { get; set; }
       public ICollection<PhotoDto> photos { get; set; }
        public ICollection<FolderDto> folders { get; set; }
        public string photoUrl { get; set; }
     
    }
}