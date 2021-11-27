using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DL
{
    public interface IDLRepo
    {
        //User CRUD
        Task<User> AddUserAsync(User user);
        Task<User> GetOneUserAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task DeleteUserAsync(int id);
        Task<User> UpdateUserAsync(User user);

        //Followings CRUD
        Task<Followings> AddFollowerAsync(Followings follower);
        Task DeleteFollowerAsync(int id);
        Task<Followings> GetOneFollowerAsync(int id);

    }
}
