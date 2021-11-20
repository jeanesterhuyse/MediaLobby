using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Folders")]
    public class Folders
    {
        public int id { get; set; }
        public string folderName { get; set; }
        public int appUserId { get; set; }
        public AppUser appUser {get; set; }
        public ICollection<Photo> photos {get; set;}

    }
}