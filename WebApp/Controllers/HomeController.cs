using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.Data;

namespace WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly WebAppContext _context;

        public HomeController(WebAppContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User user = _context.Users.FirstOrDefault(u=>u.Email.Equals(HttpContext.Session.GetString("User")));           
            List<MenuItem> itemList = GetMenus();
            var data = JsonSerializer.Serialize(new UserMenuItemViewModel { MenuItems = itemList, User = user});
            TempData["LayoutObj"] = data;
            TempData.Keep(data);
           
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
        [NonAction]
       private List<MenuItem> GetMenus()
        {
           return _context.Set<MenuItem>().ToList();
        }

        public IActionResult Menu()
        {
            return View();
        }
    }
}