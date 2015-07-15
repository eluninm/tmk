using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Telemedicine.Core.Identity;
using Telemedicine.Web.Model;
using Microsoft.AspNet.Identity;
using System;

namespace Telemedicine.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly SiteUserManager _userManager;
        private readonly SiteSignInManager _signInManager;

        public AccountController(
            IAuthenticationManager authenticationManager,
            SiteUserManager userManager,
            SiteSignInManager signInManager)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        public ActionResult LogOff()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Redirect");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Redirect");
        }
    }
}