using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    
    public class CategoriesController : Controller
    {
        private readonly CategoriesServices _categoriesServices;

        public CategoriesController(CategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid) 
            {
                _categoriesServices.Create(category);
                return View(category);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
