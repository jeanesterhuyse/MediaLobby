using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName  { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}