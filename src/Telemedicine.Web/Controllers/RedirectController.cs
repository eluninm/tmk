using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Consts;
using Telemedicine.Core.Identity;

namespace Telemedicine.Web.Controllers
{
    [Authorize]
    public class RedirectController : Controller
    {
        public ActionResult Index()
        {
            //role based redirection
            if (User.IsInRole(UserRoleNames.Patient))
            {
                return Redirect("/Patient");
            }
            if (User.IsInRole(UserRoleNames.Doctor))
            {
                return Redirect("/Doctor");
            }
            if (User.IsInRole(UserRoleNames.Administrator))
            {
                return RedirectToAction("Index", "Doctor", new { area = "Administrator" });
            }

            return View();
        }
    }
}