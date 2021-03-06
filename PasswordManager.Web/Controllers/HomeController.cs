using PasswordManager.Domain;
using PasswordManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasswordManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private static ILogic _logic = new TestLogic();

        public ActionResult Index()
        {
            //Session["user_id"] = "00000000-0000-0000-0000-000000000005";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Auth(Guid userID)
        {
            Session["user_id"] = userID.ToString();
            return RedirectToAction("Overview");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Overview()
        {
            ViewBag.Message = "Overview";


            var entries = _logic.GetFromUser(GetIdFromUser()).ToList();

            var overviewObjects = new List<OverviewObject>();

            entries.ForEach(a => overviewObjects.Add(a));

            return View(new OverviewModel()
            {
                Entries = overviewObjects
            });
        }

        public ActionResult Edit(string key)
        {
            var a = _logic.GetFromUser(GetIdFromUser())
                .Where(entry =>
                (
                    entry.Key == key &&
                    entry.UserId == GetIdFromUser()
                ));

            return (a.Any()) ? View(a.First()) : View("../Shared/Error");
        }

        public ActionResult Delete(string key)
        {
            var a = _logic.GetFromUser(GetIdFromUser())
                .Where(entry => entry.Key == key);

            return (a.Any()) ? View(a.First()) : View("../Shared/Error");
        }

        public ActionResult Details(string key)
        {
            var a = _logic.GetFromUser(GetIdFromUser())
                .Where(entry => entry.Key == key);

            return (a.Any()) ? View(a.First()) : View("../Shared/Error");
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditSubmit(Entry entry)
        {
            //TODO update
            _logic.Update(entry);
            return RedirectToAction(("Overview"));
        }

        [HttpPost]
        public ActionResult SubmitDelete(Entry entry)
        {
            //TODO delete
            _logic.Remove(entry);
            return RedirectToAction("Overview");
        }

        [HttpPost]
        public ActionResult SubmitAdd(AddModel addModel)
        {
            if(ModelState.IsValid && _logic.GetFromUser(GetIdFromUser())
                .Where(a => a.Key == addModel.Key).Count() == 0 &&
                GetIdFromUser() != Guid.Empty)
            {
                Entry entry = new Entry()
                {
                    Changed = DateTime.Now,
                    Email = addModel.Email,
                    Key = addModel.Key,
                    Notes = addModel.Notes,
                    Password = addModel.Password,
                    UserId = GetIdFromUser()
                };
                _logic.Add(entry);
                return RedirectToAction("Overview");
            }

            ViewBag.Feedback = "used key";
            return View("add", model: addModel);
        }

        private Guid GetIdFromUser()
        {
            if (Session["user_id"] == null) return Guid.Empty;

            return Guid.Parse(Session["user_id"].ToString());
        }
    }
}