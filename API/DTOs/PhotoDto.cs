using System.Collections.Generic;
namespace API.DTOs
{
    public class PhotoDto
    {
        public int id { get; set; }
        public int foldersId { get; set; }
        public string url { get; set; }
        public bool isMain { get; set; }
        public ICollection<MetaDataDto> MetaData {get; set;}
    }
}