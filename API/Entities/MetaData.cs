using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("MetaData")]
    public class MetaData
    {
        public int id { get; set; }
        public int photoid { get; set; }
        public string location { get; set; }
        public string tags { get; set; }
        public string date { get; set; }
        public string capturedBy { get; set; } 
    }
}