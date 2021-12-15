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
    public class PromoCodesController : ControllerBase
    {
        readonly private IBLRepo _bl;

        public PromoCodesController(IBLRepo bl)
        {
            _bl = bl;
        }

        // GET api/<PromoCodesController>/5
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            int result = await _bl.CheckPromoCodeAsync(code);
            
            if(result == 1)
            {
                return Ok(1);
            }
            else
            {
                return Ok(0);
            }
        }

        // POST api/<PromoCodesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PromoCode newCode)
        {
            PromoCode code = await _bl.AddPromoCodeAsync(newCode);
            return Created("api/[controller]", code);
        }
    }
}
