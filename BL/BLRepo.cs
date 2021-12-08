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

        //--------------- User CRUD ---------------
        public async Task<User> AddUserAsync(User user)
        {
            return await _repo.AddUserAsync(user);
        }
        public async Task<User> GetOneUserAsync(int id)
        {
            return await _repo.GetOneUserAsync(id);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _repo.GetAllUsersAsync();
        }

        public async Task<int> CheckUserNameAsync(string username)
        {
            return await _repo.CheckUserNameAsync(username);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _repo.DeleteUserAsync(id);
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            return await _repo.UpdateUserAsync(user);
        }

        //--------------- Following CRUD ---------------
        public async Task<Followings> AddFollowerAsync(Followings follower)
        {
            return await _repo.AddFollowerAsync(follower);
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
