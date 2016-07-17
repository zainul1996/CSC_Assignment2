using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSC_Assignment2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult UploadFile()
        {
            ViewBag.Title = "Upload File";
            return View();
        }
        
        public ActionResult Albums(string id)
        {
            ViewBag.Title = "Albums";
            ViewBag.AlbumId = id;

            return View();
        }
    }
}
