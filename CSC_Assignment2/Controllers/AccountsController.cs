using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSC_Assignment2.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Register()
        {
            ViewBag.Title = "Register";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";

            return View();
        }
    }
}