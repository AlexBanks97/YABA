using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Tab.DTO;
using Yaba.Common.User.DTO;

namespace Yaba.Web.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET api/tabs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _repository.FindAll();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var user = await _repository.Find(Id);
            if (user == null) return NotFound(); // Returns 404
            return Ok(user); // Returns 200
        }

	    [HttpGet]
	    [Route("{FbId}")]
	    public async Task<IActionResult> GetFromFbId(string FbId)
	    {
			var user = await _repository.FindFromFacebookId(FbId);
		    if (user == null) return NotFound(); // Returns 404
		    return Ok(user); // Returns 200
		}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // returns 404
            var guid = await _repository.CreateUser(user);
            return CreatedAtAction(nameof(Get), new { Id = guid }, null);
        }


    }
}
