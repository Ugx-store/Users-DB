using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using DL;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReviewsController : ControllerBase
    {
        private readonly IBLRepo _bl;

        public UserReviewsController(IBLRepo bl)
        {
            _bl = bl;
        }

        // GET api/<UserReviewsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserReviews review = await _bl.GetOneReviewAsync(id);
            if(review != null)
            {
                return Ok(review);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<UserReviewsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserReviews newReview)
        {
            UserReviews review = await _bl.AddReviewAsync(newReview);
            return Created("api/[controller]", review);
        }

        // PUT api/<UserReviewsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserReviews updatedReview)
        {
            UserReviews review = await _bl.UpdateReviewAsync(updatedReview);
            return Ok(review);
        }

        // DELETE api/<UserReviewsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bl.DeleteReviewAsync(id);
        }
    }
}
