﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace DL
{
    public class DLRepo : IDLRepo
    {
        readonly private UserDBContext _context;
        readonly private SmtpSettings _smtpSettings;

        public DLRepo(UserDBContext context, IOptions<SmtpSettings> smtpSettings)
        {
            _context = context;
            _smtpSettings = smtpSettings.Value;
        }

        //Send email to the users
        public async Task SendEmailAsync(string recipientEmail, string recipientName)
        {
            var client = new SendGridClient(_smtpSettings.ApiKey);
            var from = new EmailAddress("peterclaver.kimuli@gmail.com", "Refit"); 
            var subject = "Welcome to Refit: Let's work on your Wardrobe!";
            var to = new EmailAddress(recipientEmail, recipientName);

            var plainTextContext = $"Welcome {recipientName}";
            var htmlContext = $"<p>Welcome {recipientName}</p> <h3 style='color:red'>REFIT</h3>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContext, htmlContext);

            var response = await client.SendEmailAsync(msg);
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
        public async Task<User> GetOneUserAsync(string username)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(f => f.Followings)
                .Include(r => r.Reviews)
                .Include(p => p.ProfilePicture)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    ProfileVisits = u.ProfileVisits,
                    ReceiveEmailConsent = u.ReceiveEmailConsent,
                    FacebookLink = u.FacebookLink,
                    TwitterLink = u.TwitterLink,
                    InstagramLink = u.InstagramLink,
                    DateTimeJoined = u.DateTimeJoined,
                    ProfilePicture = _context.ProfilePictures.Where(p => p.Username == u.Username).Select(p => new ProfilePicture()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Username = p.Username,
                        ImageData = p.ImageData
                    }).ToList(),
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList(),
                    Reviews = _context.UserReviews.Where(r => r.RatedUserId == u.Id).Select(r => new UserReviews()
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        RatedUserId = r.RatedUserId,
                        RaterUserId = r.RaterUserId,
                        RaterName = r.RaterName,
                        Note = r.Note
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == username);
        }

        public async Task<int> CheckUserNameAsync(string username)
        {
            User user = await _context.Users
                .AsNoTracking()
                .Select(u => new User()
                {
                    Username = u.Username,
                })
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> CheckEmailAsync(string email)
        {
            User user = await _context.Users
                .AsNoTracking()
                .Select(u => new User()
                {
                    Email = u.Email,
                })
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> CheckPhoneNumberAsync(string phoneNumber)
        {
            User user = await _context.Users
                .AsNoTracking()
                .Select(u => new User()
                {
                    PhoneNumber = u.PhoneNumber
                })
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

            if (user != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Retrieve all users from the DB
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(f => f.Followings)
                .Include(r => r.Reviews)
                .Include(p => p.ProfilePicture)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    ProfileVisits = u.ProfileVisits,
                    ReceiveEmailConsent = u.ReceiveEmailConsent,
                    FacebookLink = u.FacebookLink,
                    TwitterLink = u.TwitterLink,
                    InstagramLink = u.InstagramLink,
                    DateTimeJoined = u.DateTimeJoined,
                    ProfilePicture = _context.ProfilePictures.Where(p => p.Username == u.Username).Select(p => new ProfilePicture()
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Username = p.Username,
                        ImageData = p.ImageData
                    }).ToList(),
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList(),
                    Reviews = _context.UserReviews.Where(r => r.RatedUserId == u.Id).Select(r => new UserReviews()
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        RatedUserId = r.RatedUserId,
                        RaterUserId = r.RaterUserId,
                        RaterName = r.RaterName,
                        Note = r.Note
                    }).ToList()
                }).ToListAsync();
        }

        //Delete a user 
        public async Task DeleteUserAsync(string username)
        {
            _context.Users.Remove(await GetOneUserAsync(username));
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
                ProfileVisits = user.ProfileVisits,
                ReceiveEmailConsent = user.ReceiveEmailConsent,
                FacebookLink = user.FacebookLink,
                TwitterLink = user.TwitterLink,
                InstagramLink = user.InstagramLink,
                DateTimeJoined = user.DateTimeJoined
            };
        }

        //Add PromoCode
        public async Task<PromoCode> AddPromoCodeAsync(PromoCode code)
        {
            await _context.AddAsync(code);
            _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return code;
        }

        //Check for a promo code
        public async Task<int> CheckPromoCodeAsync(string codeToCheck)
        {
            PromoCode code = await _context.PromoCodes
                                .AsNoTracking()
                                .Select(p => new PromoCode()
                                {
                                    Id = p.Id,
                                    Code = p.Code,
                                    Owner = p.Owner
                                })
                                .FirstOrDefaultAsync(p => p.Code == codeToCheck);
            if(code != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Add a follower
        public async Task<Followings> AddFollowerAsync(Followings follower)
        {
            await _context.AddAsync(follower);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return follower;
        }

        //Returns all users that are followed by a certain user
        public async Task<List<User>> GetUserFollows(string username)
        {
            List<Followings> followedUsers = await _context.Followings
                .Where(f => f.FollowerName == username)
                .Select(f => new Followings()
                {
                    FollowedUserId = f.FollowedUserId
                }).ToListAsync();

            List<User> users = new List<User>();

            foreach(Followings followedUser in followedUsers)
            {
                User returnedUser = await _context.Users
                                .AsNoTracking()
                                .Include(p => p.ProfilePicture)
                                .Select(u => new User()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Username = u.Username,
                                    Email = u.Email,
                                    Bio = u.Bio,
                                    PhoneNumber = u.PhoneNumber,
                                    ProfileVisits = u.ProfileVisits,
                                    ReceiveEmailConsent = u.ReceiveEmailConsent,
                                    FacebookLink = u.FacebookLink,
                                    TwitterLink = u.TwitterLink,
                                    InstagramLink = u.InstagramLink,
                                    DateTimeJoined = u.DateTimeJoined,
                                    ProfilePicture = _context.ProfilePictures.Where(p => p.Username == u.Username).Select(p => new ProfilePicture()
                                    {
                                        Id = p.Id,
                                        UserId = p.UserId,
                                        Username = p.Username,
                                        ImageData = p.ImageData
                                    }).ToList(),
                                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                                    {
                                        Id = f.Id,
                                        FollowerUserId = f.FollowerUserId,
                                        FollowedUserId = f.FollowedUserId,
                                        FollowerName = f.FollowerName
                                    }).ToList()
                                })
                                .FirstOrDefaultAsync(u => u.Id == followedUser.FollowedUserId);
                users.Add(returnedUser);
            }

            return users;
        }

        //Returns the profile information of all followers of a specific user
        public async Task<List<User>> GetUserFollowersProfiles(string username)
        {
            User user = await _context.Users
                .AsNoTracking()
                .Select(u => new User()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Username == username);

            List<User> users = new List<User>();

            foreach (Followings follower in user.Followings)
            {
                User userReturned = await _context.Users
                    .Include(p => p.ProfilePicture)
                    .Select(u => new User()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Username = u.Username,
                        Email = u.Email,
                        ProfilePicture = _context.ProfilePictures.Where(p => p.Username == u.Username).Select(p => new ProfilePicture()
                        { 
                            Id = p.Id,
                            UserId = p.UserId,
                            Username = p.Username,
                            ImageData = p.ImageData
                        }).ToList(),
                        Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                        {
                            Id = f.Id,
                            FollowerUserId = f.FollowerUserId,
                            FollowedUserId = f.FollowedUserId,
                            FollowerName = f.FollowerName
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(u => u.Username == follower.FollowerName);

                users.Add(userReturned);
            }

            return users;
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

        //Add a review
        public async Task<UserReviews> AddReviewAsync(UserReviews review)
        {
            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return review;
        }
        public async Task<UserReviews> GetOneReviewAsync(int id)
        {
            return await _context.UserReviews
                .AsNoTracking()
                .Select(r => new UserReviews()
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Note = r.Note,
                    RatedUserId = r.RatedUserId,
                    RaterUserId = r.RaterUserId,
                    RaterName = r.RaterName
                })
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task DeleteReviewAsync(int id)
        {
            _context.UserReviews.Remove(await GetOneReviewAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
        public async Task<UserReviews> UpdateReviewAsync(UserReviews review)
        {
            _context.UserReviews.Update(review);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new UserReviews()
            {
                Id = review.Id,
                Rating = review.Rating,
                Note = review.Note,
                RatedUserId = review.RatedUserId,
                RaterUserId = review.RaterUserId,
                RaterName = review.RaterName
            };
        }

        //------------------ ProfilePicture CRUD --------------------
        //Add a profile picture
        public async Task<ProfilePicture> AddProfilePicAsync(ProfilePicture pic)
        {

            ProfilePicture image = await GetProfilePicAsync(pic.Username);
            if (image != null)
            {
                pic.Id = image.Id;
                await UpdateProfilePicAsync(pic);
            }
            else
            {
                await _context.AddAsync(pic);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            return pic;
        }

        //Get one profile picture for a given username
        public async Task<ProfilePicture> GetProfilePicAsync(string username)
        {
            return await _context.ProfilePictures
                .AsNoTracking()
                .Select(p => new ProfilePicture()
                {
                    Id = p.Id,
                    Username = p.Username,
                    ImageData = p.ImageData
                })
                .FirstOrDefaultAsync(p => p.Username == username);
        }

        //Get profile pictures for followers of a given username
        public async Task<List<ProfilePicture>> GetProfilePicturesAsync(string username)
        {
            User user = await _context.Users
                .AsNoTracking()
                .Select(u => new User()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Followings = _context.Followings.Where(f => f.FollowedUserId == u.Id).Select(f => new Followings()
                    {
                        Id = f.Id,
                        FollowerUserId = f.FollowerUserId,
                        FollowedUserId = f.FollowedUserId,
                        FollowerName = f.FollowerName
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Username == username);

            List<ProfilePicture> pictures = new List<ProfilePicture>();

            foreach(Followings follower in user.Followings)
            {
                ProfilePicture pic = await _context.ProfilePictures
                                .AsNoTracking()
                                .Select(p => new ProfilePicture()
                                {
                                    Id = p.Id,
                                    Username = p.Username,
                                    ImageData = p.ImageData
                                })
                                .FirstOrDefaultAsync(p => p.Username == follower.FollowerName);
                pictures.Add(pic);
            }

            return pictures;
        }

        //Delete a specific profile pic
        public async Task DeleteProfilePicAsync(string username)
        {
            _context.ProfilePictures.Remove(await GetProfilePicAsync(username));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
        
        //Update a user's profile picture
        public async Task<ProfilePicture> UpdateProfilePicAsync(ProfilePicture pic)
        {
            _context.ProfilePictures.Update(pic);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new ProfilePicture()
            {
                Id = pic.Id,
                Username = pic.Username,
                ImageData = pic.ImageData
            };
        }
    }
}
