using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProfilePicture
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] ImageData { get; set; }
    }
}
