using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MetaDataDto
    {
        public int id { get; set; }   
        public int photoid { get; set; }
        public string location { get; set; }
        public string tags { get; set; }
        public string date { get; set; }
        public string capturedBy { get; set; }
    }
}