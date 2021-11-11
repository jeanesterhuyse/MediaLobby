using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int id { get; set; }
        public string userName  { get; set; }
        public string userEmail { get; set; }
        public byte[] userPasswordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public ICollection<Photo> photos {get; set;}
    }
}