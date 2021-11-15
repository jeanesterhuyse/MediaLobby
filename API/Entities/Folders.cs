using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
     [Table("Folders")]
    public class Folders
    {
        public int id { get; set; }
        public string folderName { get; set; }
        public ICollection<Photo> photos {get; set;}

    }
}