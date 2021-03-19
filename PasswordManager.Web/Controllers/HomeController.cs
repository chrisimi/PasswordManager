﻿using PasswordManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasswordManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Pandel()
        {
            ViewBag.Message = "The Pandel";

            return View(new PandelModel()
            {
                Entries = new List<Domain.Entry>()
                {
                    new Domain.Entry()
                    {
                        Key = "google.com",
                        Email = "chrisi@gmail.com",
                        Password = "pwd1"
                    }
                }
            });
        }
    }
}