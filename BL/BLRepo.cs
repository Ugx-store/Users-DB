using System.Collections.Generic;
using System.Threading.Tasks;
using DL;
using Models;

namespace BL
{
    public class BLRepo : IBLRepo
    {
        readonly private IDLRepo _repo;

        public BLRepo(IDLRepo repo)
        {
            _repo = repo;
        }

        //--------------- Email CRUD ---------------
        public async Task SendEmailAsync(string recipientEmail, string recipientName)
        {
            await _repo.SendEmailAsync(recipientEmail, recipientName);
        }

        //--------------- User CRUD ---------------
        public async Task<User> AddUserAsync(User user)
        {
            return await _repo.AddUserAsync(user);
        }
        public async Task<User> GetOneUserAsync(string username)
        {
            return await _repo.GetOneUserAsync(username);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repo.GetAllUsersAsync();
        }

        public async Task<int> CheckUserNameAsync(string username)
        {
            return await _repo.CheckUserNameAsync(username);
        }

        public async Task<int> CheckEmailAsync(string email)
        {
            return await _repo.CheckEmailAsync(email);
        }

        public async Task<int> CheckPhoneNumberAsync(string phoneNumber)
        {
            return await _repo.CheckPhoneNumberAsync(phoneNumber);
        }


        public async Task DeleteUserAsync(string username)
        {
            await _repo.DeleteUserAsync(username);
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            return await _repo.UpdateUserAsync(user);
        }

        //--------------- Promocodes CRUD ---------------
        public async Task<PromoCode> AddPromoCodeAsync(PromoCode code)
        {
            return await _repo.AddPromoCodeAsync(code);
        }
        public async Task<int> CheckPromoCodeAsync(string code)
        {
            return await _repo.CheckPromoCodeAsync(code);
        }

        //--------------- Following CRUD ---------------
        public async Task<Followings> AddFollowerAsync(Followings follower)
        {
            return await _repo.AddFollowerAsync(follower);
        }

        public async Task<List<string>> GetUserFollows(string username)
        {
            return await _repo.GetUserFollows(username);
        }

        public async Task<Followings> GetOneFollowerAsync(int id)
        {
            return await _repo.GetOneFollowerAsync(id);
        }

        public async Task DeleteFollowerAsync(int id)
        {
            await _repo.DeleteFollowerAsync(id);
        }

        //--------------- Reviews CRUD ---------------
        public async Task<UserReviews> AddReviewAsync(UserReviews review)
        {
            return await _repo.AddReviewAsync(review);
        }
        public async Task<UserReviews> GetOneReviewAsync(int id)
        {
            return await _repo.GetOneReviewAsync(id);
        }
        public async Task DeleteReviewAsync(int id)
        {
            await _repo.DeleteReviewAsync(id);
        }
        public async Task<UserReviews> UpdateReviewAsync(UserReviews review)
        {
            return await _repo.UpdateReviewAsync(review);
        }

        //--------------- Profile Picture CRUD ---------------
        public async Task<ProfilePicture> AddProfilePicAsync(ProfilePicture pic)
        {
            return await _repo.AddProfilePicAsync(pic);
        }
        public async Task<ProfilePicture> GetProfilePicAsync(int id)
        {
            return await _repo.GetProfilePicAsync(id);
        }
        public async Task DeleteProfilePicAsync(int id)
        {
            await _repo.DeleteProfilePicAsync(id);
        }
        public async Task<ProfilePicture> UpdateProfilePicAsync(ProfilePicture pic)
        {
            return await _repo.UpdateProfilePicAsync(pic);
        }
    }
}
