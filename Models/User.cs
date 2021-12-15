using System;
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
        public string PhoneNumber { set; get; }
        public bool ReceiveEmailConsent { set; get; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string PromoCode { get; set; }
        public DateTime DateTimeJoined { set; get; }
        public List<Followings> Followings { set; get; }
        public List<UserReviews> Reviews { set; get; }
    }
}
