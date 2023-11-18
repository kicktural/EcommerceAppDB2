using Entities.Concreate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.ViewComponents
{
    public class UserAuthViewComponent : ViewComponent
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public UserAuthViewComponent(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId =  _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user =await _userManager.FindByIdAsync(userId);
            return View("UserAuth", user);
        }
    }
}
