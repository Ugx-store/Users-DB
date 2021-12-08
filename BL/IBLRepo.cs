using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BL
{
    public interface IBLRepo
    {
        //User CRUD
        Task<User> AddUserAsync(User user);
        Task<User> GetOneUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<int> CheckUserNameAsync(string username);
        Task DeleteUserAsync(int id);
        Task<User> UpdateUserAsync(User user);

        //Following CRUD
        Task<Followings> AddFollowerAsync(Followings follower);
        Task DeleteFollowerAsync(int id);
        Task<Followings> GetOneFollowerAsync(int id);

        //User Reviews CRUD
        Task<UserReviews> AddReviewAsync(UserReviews review);
        Task<UserReviews> GetOneReviewAsync(int id);
        Task DeleteReviewAsync(int id);
        Task<UserReviews> UpdateReviewAsync(UserReviews review);

        //ProfilePicture CRUD
        Task<ProfilePicture> AddProfilePicAsync(ProfilePicture pic);
        Task<ProfilePicture> GetProfilePicAsync(int id);
        Task DeleteProfilePicAsync(int id);
        Task<ProfilePicture> UpdateProfilePicAsync(ProfilePicture pic);
    }
}
