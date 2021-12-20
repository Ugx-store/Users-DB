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
    public class EmailSender : ControllerBase
    {
        private readonly IBLRepo _bl;

        public EmailSender(IBLRepo bl)
        {
            _bl = bl;
        }

        // POST api/<EmailSender>
        [HttpGet("{recipientEmail}/{recipientName}")]
        public async Task Post(string recipientEmail, string recipientName)
        {
            try
            {
                await _bl.SendEmailAsync(recipientEmail, recipientName);
            }
            catch(Exception e)
            {
                BadRequest(e?.InnerException?.InnerException?.Message ?? e?.InnerException?.Message ?? e?.Message);
            }
        }
    }
}
