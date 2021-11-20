using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int id { get; set; }
        public string url { get; set; }
        public bool isMain { get; set; }
        public string publicId { get; set; }
        public AppUser appUser {get; set; }
        public Folders folder {get; set; }
        public int AppUserId { get; set; }
        public int foldersId { get; set; }
    }
}