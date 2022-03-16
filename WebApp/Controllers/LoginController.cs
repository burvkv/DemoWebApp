using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.Data;
using Microsoft.AspNetCore.Session;

namespace WebApp.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly WebAppContext _context;

        public LoginController(WebAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserForLoginDto user)
        {
            User _user = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
          
            if (_user != null)
            {
                var claims = new List<Claim>
              {
                  new Claim(ClaimTypes.Name,_user.Email)
              };
                var userIdentity = new ClaimsIdentity(claims,"Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                HttpContext.Session.SetString("User",_user.Email);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("Index");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Index");

        }
    }
}
