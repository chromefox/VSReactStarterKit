using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExperimentWeb.Services;

namespace ExperimentWeb.Controllers
{
    public class SignalRTestController : Controller
    {
        // GET: SignalRTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAppendTable()
        {
            return View();
        }

        public ActionResult SpecificObject(long id)
        {
            ViewBag.ObjectId = id;
            return View();
        }


        [HttpPost]
        public ActionResult TestBroadcastOnOtherObjects()
        {
            var hubService = new HubService();
            hubService.TestSend("from server push", "server hello!");
            return RedirectToAction("Index");
        }
    }
}