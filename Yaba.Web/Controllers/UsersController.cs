using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Tab.DTO;

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
    }
}
