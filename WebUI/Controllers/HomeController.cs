using Business.Abstract;
using Entities.Concreate;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private  readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllFeaturedProduts("az-Az");
            var categoryes = _categoryService.GetAllFeaturedCategory("Az");
            var recent = _productService.GetAllRecentProduts("az-Az");
            HomeVM homeVM = new()
            {
                ProductFeaturedDTOs = products.Data,
                CategoryFeaturedDTOs = categoryes.Data,
                ProductRecentDTO = recent.Data,
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}