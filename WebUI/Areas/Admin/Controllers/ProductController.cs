using Business.Abstract;
using Core.DataAccess;
using Core.DataAccess.EntityFremeworkCore;
using DataAccess.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Security.Claims;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
   public class ProductController : Controller
    {

        private readonly IProductService _productservice;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productservice, IHttpContextAccessor contextAccessor, ICategoryService categoryService)
        {
            _productservice = productservice;
            _contextAccessor = contextAccessor;
            _categoryService = categoryService;
         
        }

        public IActionResult Index()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result =  _productservice.GetDashboardProductS(userId, "az-Az");
            return View(result.Data);
        }

        public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories("Eng");
            ViewBag.Categoryes = new SelectList(categories.Data, "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductAddDTO productAddDTO)
        {

            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _productservice.AddProductAdmin(userId, productAddDTO);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
