using System.Collections.Generic;

namespace API.DTOs
{
    public class FolderDto
    {
        public int id { get; set; }
        public string folderName { get; set; }
        public ICollection<PhotoDto> photos {get; set;}
    }
}