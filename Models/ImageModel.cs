using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ImageModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IFormFile ImageData { get; set; }

        
    }
}
