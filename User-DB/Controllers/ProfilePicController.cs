using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicController : ControllerBase
    {

        readonly private IBLRepo _bl;

        public ProfilePicController(IBLRepo bl)
        {
            _bl = bl;
        }

        // GET api/<ProfilePicController>/5; To return one Profile by ID
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            ProfilePicture pic = await _bl.GetProfilePicAsync(username);
            if (pic != null)
            {
                return Ok(pic);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<ProfilePicController>; To add a new profile pic in the DB
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfilePicture newPic)
        {
            ProfilePicture pic = await _bl.AddProfilePicAsync(newPic);
            return Created("api/[controller]", pic);
        }

        // PUT api/<ProfilePicController>/5; To update a profile pic in the DB
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProfilePicture updatedPic)
        {
            ProfilePicture pic = await _bl.UpdateProfilePicAsync(updatedPic);
            return Ok(pic);
        }

        // DELETE api/<ProfilePicController>/5; To delete a picture from the DB
        [HttpDelete("{id}")]
        public async Task Delete(string username)
        {
            await _bl.DeleteProfilePicAsync(username);
        }
    }
}
