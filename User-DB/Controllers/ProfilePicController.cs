﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;
using System.IO;
using System.Collections.Generic;

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

        // GET api/<ProfilePicController>/johndoe; To return one Profile picture by username
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            ProfilePicture pic = await _bl.GetProfilePicAsync(username);
            if (pic != null)
            {
                return Ok(pic.ImageData);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<ProfilePicController>/johndoe; To return a list of Profile pictures by username
        [HttpGet("pictures/{username}")]
        public async Task<IActionResult> GetProfilePictures(string username)
        {
            List<ProfilePicture> pics = await _bl.GetProfilePicturesAsync(username);
            if (pics != null)
            {
                return Ok(pics);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<ProfilePicController>; To add a new profile pic in the DB
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfilePicture pic)
        {
            /*var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            byte[] image;

            if (file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    image = memoryStream.ToArray();

                    ProfilePicture pic = await _bl.AddProfilePicAsync(username, image);
                }
            }*/

            ProfilePicture newPic = await _bl.AddProfilePicAsync(pic);

            if(newPic != null)
            {
                return Created("api/[controller]", pic.Id);
            }
            else
            {
                return Created("api/[controller]", "failure");
            }
            
        }

        // PUT api/<ProfilePicController>/5; To update a profile pic in the DB
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProfilePicture updatedPic)
        {
            ProfilePicture pic = await _bl.UpdateProfilePicAsync(updatedPic);
            return Ok(pic);
        }

        // DELETE api/<ProfilePicController>/5; To delete a picture from the DB
        [HttpDelete("{username}")]
        public async Task Delete(string username)
        {
            await _bl.DeleteProfilePicAsync(username);
        }
    }
}
