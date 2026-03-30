using MagicVillaWeb.Model;
using MagicVillaWeb.Model.DTO;
using MagicVillaWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService _authService)
        {
            this._authService = _authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new LoginRequestDTO();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO obj)
        {
            ApiResponse result = await _authService.Register<ApiResponse>(obj);

            if (result!= null && result.isSuccess)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
