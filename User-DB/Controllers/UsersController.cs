using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly private IBLRepo _bl;

        public UsersController(IBLRepo bl)
        {
            _bl = bl;
        }

        // GET: api/<UsersController>; To return all users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<User> users = await _bl.GetAllUsersAsync();
            if(users.Count != 0)
            {
                return Ok(users);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<UsersController>/5; To return one user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _bl.GetOneUserAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<UsersController>/user/peter; To return either 1 or 0
        [HttpGet("user/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            int num = await _bl.CheckUserNameAsync(username);
            if (num == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        // GET api/<UsersController>/email/peter@test.com; To return either 1 or 0
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetEmail(string email)
        {
            int num = await _bl.CheckEmailAsync(email);
            if (num == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        // GET api/<UsersController>/phone/123456789; To return either 1 or 0
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetPhone(string phoneNumber)
        {
            int num = await _bl.CheckPhoneNumberAsync(phoneNumber);
            if (num == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        // POST api/<UsersController>; To create a new user in the DB
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            User user = await _bl.AddUserAsync(newUser);
            return Created("api/[controller]", user);
        }

        // PUT api/<UsersController>/5; To update a user in the DB
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User updatedUser)
        {
            User user = await _bl.UpdateUserAsync(updatedUser);
            return Ok(user);
        }

        // DELETE api/<UsersController>/5; To delete a user from the DB
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bl.DeleteUserAsync(id);
        }
    }
}
