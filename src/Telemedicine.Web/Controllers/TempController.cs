using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telemedicine.Core.Domain.Services;

namespace Telemedicine.Web.Controllers
{
    public class TempController : Controller
    {
        private readonly IConversationService _messageService;

        public TempController(IConversationService messageService)
        {
            _messageService = messageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult Testm1(string id)
        {
            ViewBag.SelectedId = id;
            return PartialView();
        }

        public ActionResult Demo2()
        {
            return View();
        }

        public ActionResult Clear()
        {
            return RedirectToAction("Index", "Redirect");
        }
    }
}