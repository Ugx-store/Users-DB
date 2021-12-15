using Microsoft.EntityFrameworkCore;
using Models;

namespace DL
{
    public class UserDBContext : DbContext
    {
        public UserDBContext() : base() { }
        public UserDBContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Followings> Followings { get; set; }
        public DbSet<UserReviews> UserReviews { get; set; }
        public DbSet<ProfilePicture> ProfilePictures { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
    }
}
