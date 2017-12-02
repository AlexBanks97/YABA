using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.Tab;

namespace Yaba.Web.Controllers
{
    public class TabItemCategoriesController : Controller
    {
		private readonly ITabItemCategoryRepository _repository;

		public TabItemCategoriesController(ITabItemCategoryRepository repository)
		{
			_repository = repository;
		}



	}
}
