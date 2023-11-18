using Entities.Concreate;
using Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var CheckEmail = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (CheckEmail == null)
            {
                ModelState.AddModelError("Error", "Create Email!");
                return View(loginDTO);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(CheckEmail, loginDTO.Password, loginDTO.RememberMe, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Error", "Error Password, Email!!!");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Email!");
                return View(registerDTO);
            }


            var checkEmail = await _userManager.FindByEmailAsync(registerDTO.Email);

            if (checkEmail != null)
            {
                ModelState.AddModelError("Error", "This Email Is exsis!");
                return View(registerDTO);
            }

            User user = new()
            {
                Firstname = registerDTO.FisrtName,
                UserName = registerDTO.Email,
                Lastname = registerDTO.LastName,
                PhotoUrl = "//",
                Address= "//"
            };


            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                //return RedirectToAction("Index", "Home");
                return Redirect("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Error", item.Description);
                }
                return View(registerDTO);
            }
        }

        [HttpPost]

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
