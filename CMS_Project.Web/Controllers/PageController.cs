using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using CMS_Project.Entity.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Project.Web.Controllers
{
    [Authorize(Roles = "employee")]
    [Authorize(Roles = "admin")]
    [Authorize(Roles = "member")]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;
        public PageController(IPageRepository pageRepository) => _pageRepository = pageRepository;

        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null) return View(await _pageRepository.FirstOrDefault(x => x.Slug == "home-page"));

            Page page = await _pageRepository.FirstOrDefault(x => x.Slug == slug);

            if (page == null) return NotFound();

            return View(page);
        }

    }
}
