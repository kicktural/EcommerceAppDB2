using Business.Abstract;
using Entities.Concreate;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{


    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = _categoryService.GetAllAdminCategories("Az");
            if (result.Success)
            {
                return View(result.Data);
            }
            return View();
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CategoryAddDTO categoryAddDTO)
        {
            _categoryService.AddCategory(categoryAddDTO);
            return RedirectToAction("Index");
        }
    }
}
