using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        _repository _repository = new _repository();
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePerson()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePerson(Customers cus)
        {
            if (ModelState.IsValid)
            {
                _repository.CreatePerson(cus);
                TempData["succes"] = "Succesfully saved";
                ModelState.Clear();
                return RedirectToAction("ListPersons");
            }
            return View();
        }


        public ActionResult ListPersons()
        {
            var getlist = _repository.ListPerson();
            return View(getlist);
        }

        public ActionResult UpdatePerson(string id)
        {
            var getupdate = _repository.GetUpdate(id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdatePerson(Customers cus)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdatePerson(cus);
                TempData["Succesupdate"] = "Succesfully Updated";
                return RedirectToAction("ListPersons");

            }
            return View();
        }

        public ActionResult DeletePerson(string id)
        {
            _repository.DeletePerson(id);
            TempData["Succesdel"] = "Succesfully Deleted";
            return RedirectToAction("ListPersons");

        }
        public ActionResult CheckingCommit()
        {
            return View();
        }
    }
}