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
        public FileInfo ImageData { get; set; }

        
    }
}
