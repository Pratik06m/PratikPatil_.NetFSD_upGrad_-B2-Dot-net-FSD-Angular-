using EMS.DAL.Models;
using EMS.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IGenericRepository<UserInfo> _userRepo;

        public AuthController(IGenericRepository<UserInfo> userRepo)
        {
            _userRepo = userRepo;
        }

        // GET
        public IActionResult Login()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var users = await _userRepo.GetAll();

            var user = users.FirstOrDefault(u =>
                u.EmailId == email && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            // Store session
            HttpContext.Session.SetString("UserEmail", user.EmailId);
            HttpContext.Session.SetString("UserRole", user.Role);

            if (user.Role == "Admin")
                return RedirectToAction("Index", "Event");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}