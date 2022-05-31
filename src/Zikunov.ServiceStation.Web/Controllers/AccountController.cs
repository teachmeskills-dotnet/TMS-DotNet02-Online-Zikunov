using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Zikunov.ServiceStation.WebApp.Shared.Models;
using Zikunov.ServiceStation.Web.Interfaces;

namespace Zikunov.ServiceStation.Web.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="identityService">Identity Service.</param>
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        /// <summary>
        /// Login (Get).
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new UserSignInRequest();

            return View(viewModel);
        }

        /// <summary>
        /// Login (Post).
        /// </summary>
        /// <param name="request">User login request</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserSignInRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));

            if (ModelState.IsValid)
            {
                var token = await _identityService.SignInAsync(request);
                var claims = new List<Claim> 
                {
                    new Claim(ClaimTypes.Name, token),
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            return View(request);
        }
    }
}
