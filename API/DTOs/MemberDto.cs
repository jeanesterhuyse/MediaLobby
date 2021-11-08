using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string UserName  { get; set; }
        public string UserEmail { get; set; }
        public ICollection<PhotoDto> Photos {get; set;}
        public string PhotoUrl { get; set; }

    }
}