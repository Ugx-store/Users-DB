using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DLRepo : IDLRepo
    {
        readonly private UserDBContext _context;
        public DLRepo(UserDBContext context)
        {
            _context = context;
        }

        //Add a user to the DB
        public async Task<User> AddUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return user;
        }

        //Retrieve one user from the DB
        public async Task<User> GetOneUserAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(f => f.Followings)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        //Retrieve all users from the DB
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(f => f.Followings)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList()
                }).ToListAsync();
        }

        //Delete a user 
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetOneUserAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
        
        //Update a user's profile
        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Bio = user.Bio,
                PhoneNumber = user.PhoneNumber,
            };
        }

        //Add a follower
        public async Task<Followings> AddFollowerAsync(Followings follower)
        {
            await _context.AddAsync(follower);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return follower;
        }

        //Get one follower for the delete method
        public async Task<Followings> GetOneFollowerAsync(int id)
        {
            return await _context.Followings
                .AsNoTracking()
                .Select(f => new Followings()
                {
                    Id = f.Id,
                    FollowedUserId = f.FollowedUserId,
                    FollowerUserId = f.FollowerUserId,
                    FollowerName = f.FollowerName
                })
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        //Delete a follower
        public async Task DeleteFollowerAsync(int id)
        {
            _context.Followings.Remove(await GetOneFollowerAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
    }
}
