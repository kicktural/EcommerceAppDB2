using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IproductDAL _productDal;
        public ProductController(IProductService productService, ICategoryService categoryService, IproductDAL productDal)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productDal = productDal;
        }

        public IActionResult Index(List<int> categoryIds, int page = 1)
        {
            //ViewBag.test = _categoryService.GetAllCategories();
            ViewBag.CurrentPage = page;
            ViewBag.ProductCount = _productService.GetProductCount(3, categoryIds).Data;
            var result = _productService.GetAllFiltredProducts(categoryIds, "az-Az", 0, maxPrice: 10000, pageNo: page, take: 3);
            var categoryes = _categoryService.GetAllFilterCategories("Az");
            ProductFilterVM productFilterVM = new()
            {
                ProductFilters = result.Data,
                CategoryFilters = categoryes.Data
            };
            return View(productFilterVM);
        }

        public IActionResult Detail(int id)
        {
            var result = _productService.GetProductBYId(id, "az-Az");         
            if (result.Success)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(Index), "Home");
        }


        public JsonResult GetDatas(int page, int take, string categoryList)
        {
            var categories = _categoryService.GetAllFilterCategories("Az");
            var cats = new List<int>();
            if (categoryList == null)
            {
                cats = categories.Data.Select(x => x.Id).ToList();
            }
            else
            {
                cats = categoryList.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            }


            var productCount = _productService.GetProductCount(take, cats).Data;
            var result = _productService.GetAllFiltredProducts(cats, "az-Az", 0, maxPrice: 10000, pageNo: page, take: take);
            return Json(result);
        }
    }
}
