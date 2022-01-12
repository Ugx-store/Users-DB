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
        Task<User> GetOneUserAsync(string username);
        Task<List<User>> GetAllUsersAsync();
        Task<int> CheckUserNameAsync(string username);
        Task<int> CheckEmailAsync(string email);
        Task<int> CheckPhoneNumberAsync(string phoneNumber);
        Task DeleteUserAsync(string username);
        Task<User> UpdateUserAsync(User user);

        //Emails CRUD
        Task SendEmailAsync(string recipientEmail, string recipientName);

        //Promocodes CRUD
        Task<PromoCode> AddPromoCodeAsync(PromoCode code);
        Task<int> CheckPromoCodeAsync(string code);

        //Following CRUD
        Task<Followings> AddFollowerAsync(Followings follower);
        Task<List<User>> GetUserFollows(string username);
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
