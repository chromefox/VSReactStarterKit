using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExperimentLibrary;

namespace ExperimentWeb.Controllers
{
    public class MassTransitController : Controller
    {
        // GET: MassTransit
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail()
        {
            var mail = new MailMessageContract(new string[] { "ronny.muliawan@resolutelabs.com" }, @"<p>Hello world</p>");

            MvcApplication.Bus.Publish<IMailMessageContract>(mail, x =>
            {
                x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent);
            });

            return RedirectToAction("Index");
        }
    }
}