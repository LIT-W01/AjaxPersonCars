using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxPersonCars.Data;

namespace AjaxPersonCars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPeople()
        {
            var mgr = new PersonCarsManager(Properties.Settings.Default.ConStr);
            return Json(mgr.GetAllPersons(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCarsForPerson(int personId)
        {
            var mgr = new PersonCarsManager(Properties.Settings.Default.ConStr);
            return Json(mgr.GetCarsForPerson(personId));
        }

        [HttpPost]
        public void AddCar(Car car)
        {
            var mgr = new PersonCarsManager(Properties.Settings.Default.ConStr);
            mgr.AddCar(car);
        }

        [HttpPost]
        public void DeletePerson(int personId)
        {
            var mgr = new PersonCarsManager(Properties.Settings.Default.ConStr);
            mgr.DeletePersonAndCars(personId);
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            var mgr = new PersonCarsManager(Properties.Settings.Default.ConStr);
            mgr.AddPerson(person);
            return Json(person);
        }
    }
}
