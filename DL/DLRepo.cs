using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
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
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = "Welcome to Refit: Let's work on your Wardrobe!";

            message.Body = new TextPart("html")
            {
                Text = "<h3 style='color:red'>REFIT</h3>"
            };

            var client = new SmtpClient();

            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
            await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
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
                .Include(r => r.Reviews)
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    ReceiveEmailConsent = u.ReceiveEmailConsent,
                    FacebookLink = u.FacebookLink,
                    TwitterLink = u.TwitterLink,
                    InstagramLink = u.InstagramLink,
                    DateTimeJoined = u.DateTimeJoined,
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
                .FirstOrDefaultAsync(u => u.Id == id);
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
                .Select(u => new User()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.Username,
                    Email = u.Email,
                    Bio = u.Bio,
                    PhoneNumber = u.PhoneNumber,
                    ReceiveEmailConsent = u.ReceiveEmailConsent,
                    FacebookLink = u.FacebookLink,
                    TwitterLink = u.TwitterLink,
                    InstagramLink = u.InstagramLink,
                    DateTimeJoined = u.DateTimeJoined,
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

        //ProfilePicture CRUD
        public async Task<ProfilePicture> AddProfilePicAsync(ProfilePicture pic)
        {
            await _context.AddAsync(pic);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return pic;
        }
        public async Task<ProfilePicture> GetProfilePicAsync(int id)
        {
            return await _context.ProfilePictures
                .AsNoTracking()
                .Select(p => new ProfilePicture()
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    ImageData = p.ImageData
                })
                .FirstOrDefaultAsync(p => p.UserId == id);
        }
        public async Task DeleteProfilePicAsync(int id)
        {
            _context.ProfilePictures.Remove(await GetProfilePicAsync(id));
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }
        public async Task<ProfilePicture> UpdateProfilePicAsync(ProfilePicture pic)
        {
            _context.ProfilePictures.Update(pic);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            return new ProfilePicture()
            {
                Id = pic.Id,
                UserId = pic.UserId,
                ImageData = pic.ImageData
            };
        }
    }
}
