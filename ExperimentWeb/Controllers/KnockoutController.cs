using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExperimentWeb.Models;
using Newtonsoft.Json;

namespace ExperimentWeb.Controllers
{
    public class KnockoutController : Controller
    {
        // GET: Knockout
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMails(string folder)
        {
            var mails = MvcApplication.MailSample;
            return Content(JsonConvert.SerializeObject(new MailResponse(folder, mails.Where(s => s.Folder.Equals(folder)).ToList())), "application/json");
        }
    }
}