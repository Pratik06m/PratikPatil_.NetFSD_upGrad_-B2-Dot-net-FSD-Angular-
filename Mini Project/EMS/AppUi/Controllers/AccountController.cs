using AppUi.Models;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // ─── Admin Login ────────────────────────────────────────────────────────

        [HttpGet]
        public IActionResult AdminLogin()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userRepo.AuthenticateAsync(model.EmailId, model.Password);
            if (user == null || user.Role != "Admin")
            {
                ModelState.AddModelError("", "Invalid admin credentials.");
                return View(model);
            }

            HttpContext.Session.SetString("UserEmail", user.EmailId);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Dashboard", "Admin");
        }

        // ─── Participant Login ───────────────────────────────────────────────────

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (HttpContext.Session.GetString("Role") == "Participant")
                return RedirectToAction("Dashboard", "Participant");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userRepo.AuthenticateAsync(model.EmailId, model.Password);
            if (user == null || user.Role != "Participant")
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }

            HttpContext.Session.SetString("UserEmail", user.EmailId);
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("Role", user.Role);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Dashboard", "Participant");
        }

        // ─── Register ───────────────────────────────────────────────────────────

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userRepo.EmailExistsAsync(model.EmailId))
            {
                ModelState.AddModelError("EmailId", "This email is already registered.");
                return View(model);
            }

            var user = new UserInfo
            {
                EmailId = model.EmailId,
                UserName = model.UserName,
                Password = model.Password,
                Role = "Participant"
            };

            await _userRepo.AddAsync(user);
            TempData["Success"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        // ─── Logout ─────────────────────────────────────────────────────────────

        public IActionResult Logout()
        {
            var role = HttpContext.Session.GetString("Role");
            HttpContext.Session.Clear();
            return role == "Admin"
                ? RedirectToAction("AdminLogin")
                : RedirectToAction("Login");
        }
    }
}
