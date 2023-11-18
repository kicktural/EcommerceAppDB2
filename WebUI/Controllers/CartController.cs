using Business.Abstract;
using Entities.DTOs.CardDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public JsonResult AddToCart(string id, int quantity)
        {
            var cookie = Request.Cookies["cart"];

            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";

            List<CartCookieDTO> cartCookies = new();


            CartCookieDTO cartCookieDTO = new CartCookieDTO()
            {
                Id = Convert.ToInt32(id),
                Quantity = quantity,
            };
            if (cookie == null)
            {
                cartCookies.Add(cartCookieDTO);
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(cartCookies);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            else
            {
                var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
                var findData = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

                if (findData != null)
                {
                    findData.Quantity += 1;
                }
                else
                {
                    data.Add(cartCookieDTO);
                }
                var cookieJson = JsonSerializer.Serialize<List<CartCookieDTO>>(data);
                Response.Cookies.Append("cart", cookieJson, cookieOptions);
            }
            return Json("");
        }
        public IActionResult UserCart()
        {
            return View();
        }

        public JsonResult GetProduct()
        {
            var cookie = Request.Cookies["cart"];
            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var quantity = data.Select(x => x.Quantity).ToList();
            var dataIds = data.Select(x => x.Id).ToList();
            var result = _productService.GetProductForCart(dataIds, "az-Az", quantity);
            return Json(result);
        }


        public JsonResult RemoveData(string id)
        {
            var cookie = Request.Cookies["cart"];
            var cookieOptionsRemove = new CookieOptions();
            //cookieOptionsRemove.Expires = DateTime.Now.AddDays(1);
            //cookieOptionsRemove.Path = "/";

            var data = JsonSerializer.Deserialize<List<CartCookieDTO>>(cookie);
            var result = data.FirstOrDefault(x => x.Id == Convert.ToInt32(id));

            data.Remove(result);

            var cookieJson = JsonSerializer.Serialize(data);
            Response.Cookies.Append("cart", cookieJson, cookieOptionsRemove);

            return Json("ok");
        }
    }
}
