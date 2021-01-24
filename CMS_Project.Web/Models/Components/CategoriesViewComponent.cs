using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_Project.Web.Models.Components
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesViewComponent(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<IViewComponentResult> InvokeAsync() => View(await _categoryRepository.GetAll());
    }
}
