using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using CMS_Project.Entity.Entities.Concrete;
using CMS_Project.Entity.Enums;
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
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository,
                                 ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index() => View(await _productRepository.Get(x => x.Status != Status.Passive));

        public async Task<IActionResult> ProductByCategory(string categorySlug)
        {
            Category category = await _categoryRepository.FirstOrDefault(x => x.Slug == categorySlug);

            if (category == null) return RedirectToAction("Index");

            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;
            List<Product> products = await _productRepository.Get(x => x.CategoryId == category.Id);
            return View(products);
        }
    }
}
