using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using CMS_Project.Entity.Entities.Concrete;
using CMS_Project.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Project.Web.Models.Components
{
    public class MainMenuViewComponent:ViewComponent
    {
        private readonly IPageRepository _pageRepo;

        public MainMenuViewComponent(IPageRepository pageRepository) => _pageRepo = pageRepository;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await GetPagesAsync();
            return View(pages);
        }


        public async Task<List<Page>> GetPagesAsync() => await _pageRepo.Get(x => x.Status != Status.Passive);

    }
}
