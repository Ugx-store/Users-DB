using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User { 
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Bio { set; get; }
        public long PhoneNumber { set; get; }
        public List<Followings> Followings { set; get; }
        public List<UserReviews> Reviews { set; get; }
    }
}
