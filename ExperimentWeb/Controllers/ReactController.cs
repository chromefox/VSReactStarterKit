using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExperimentWeb.Models;
using Newtonsoft.Json;

namespace ExperimentWeb.Controllers
{
    public class ReactController : Controller
    {
        // GET: React
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            // parse the comments object
            var path = Server.MapPath("~/data.json");
            var curCommentList = JsonConvert.DeserializeObject<List<Comment>>(System.IO.File.ReadAllText(path));
            curCommentList.Add(comment);
            var newContent = JsonConvert.SerializeObject(curCommentList);
            System.IO.File.WriteAllText(path, newContent);
            return Content(newContent, "application/json");
        }
    }
}