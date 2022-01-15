using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DL;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : ControllerBase
    {
        private readonly IBLRepo _bl;

        public FollowingController(IBLRepo bl)
        {
            _bl = bl;
        }

        // GET api/<FollowingController>/username; To return all users followed by a user
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            List <User> users = await _bl.GetUserFollows(username);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<FollowingController>/username; To return all user profiles of a user's followers
        [HttpGet("followers/{username}")]
        public async Task<IActionResult> GetUserFollowers(string username)
        {
            List<User> users = await _bl.GetUserFollowersProfiles(username);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<FollowingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Followings newFollower)
        {
            Followings follower = await _bl.AddFollowerAsync(newFollower);

            return Created("api/[controller]", follower);
        }

        // DELETE api/<FollowingController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bl.DeleteFollowerAsync(id);
        }
    }
}
