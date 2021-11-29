using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserReviews
    {
        [Key]
        public int Id { set; get; }
        public int Rating { set; get; }
        public string Note { set; get; }
        public int RaterUserId { set; get; }
        public string RaterName { set; get; }
        public int RatedUserId { set; get; }
    }
}
