using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;

namespace Yaba.Web.Controllers
{
    public class TabsController : Controller
    {
        private readonly ITabRepository _repository;

        public TabsController(ITabRepository repository)
        {
            _repository = repository;
        }

        // GET api/tabs
        [HttpGet]
        public async Task<IEnumerable<TabDTO>> Get()
        {
            return await _repository.FindAllTabs();
        }

        // GET api/budgets/{guid}
        public async Task<IActionResult> Get(Guid id)
        {
            var tab = await _repository.FindTab(id);
            if (tab == null) return NotFound(); // Returns 404
            return Ok(tab); // Returns 200
        }

    }
}
