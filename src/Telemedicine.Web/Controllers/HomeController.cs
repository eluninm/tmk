﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemedicine.Core.Identity;

namespace Telemedicine.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteUserManager _userManager;

        public HomeController(SiteUserManager userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult NavProfileBox()
        {
            var surrentUser = HttpContext.User.Identity.GetUserDisplayName();
            ViewBag.DisplayName = surrentUser;
            return PartialView("NavProfileBox");
        }


        public ActionResult Avatar()
        {
            var user =  _userManager.FindById(HttpContext.User.Identity.GetUserId());

            return PartialView("_Avatar", user?.AvatarUrl);
        }

        public async Task<ActionResult> UserCallDialog(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound("Пользователь не найден в базе данных.");
            }

            ViewBag.UserDisplayName = user.DisplayName;

            return PartialView("_UserCallDialog");
        }
    }
}