using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Followings
    {
        [Key]
        public int Id { set; get; }
        public int FollowerUserId { set; get; }
        public int FollowedUserId { set; get; }
        public string FollowerName { set; get; }
    }
}
