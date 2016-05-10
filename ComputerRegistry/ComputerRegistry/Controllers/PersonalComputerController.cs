using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerRegistry.Models;

namespace ComputerRegistry.Controllers
{ 
    public class PersonalComputerController : BaseController
    {
        private ComputerRegistryContext db = new ComputerRegistryContext();

        //
        // GET: /PersonalComputer/

        public ViewResult Index(string currentFilter, string searchString, string makeid)
        {
            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
                PopulateMakesDropDownList();
            }
            else
            {
                if (!String.IsNullOrEmpty(makeid))
                    PopulateMakesDropDownList(int.Parse(makeid));
                else
                    PopulateMakesDropDownList();
            }

            ViewBag.CurrentFilter = searchString;

            ViewBag.CurrentMakefilter = makeid;

            var personalcomputers = from pc in db.PersonalComputers.Include(p => p.Make) select pc;

            if (!String.IsNullOrEmpty(searchString))
            {
                personalcomputers = personalcomputers.Where(c => c.Model.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(makeid))
            {
                int make = int.Parse(makeid);
                personalcomputers = personalcomputers.Where(c => c.MakeID == make);
            }

            if (personalcomputers.Count() == 0)
                ViewBag.Message = "No records found";

            return View(personalcomputers.ToList());
        }

        //
        // GET: /PersonalComputer/Details/5

        public ViewResult Details(int id)
        {
            PersonalComputer personalcomputer = db.PersonalComputers.Find(id);
            return View(personalcomputer);
        }

        //
        // GET: /PersonalComputer/Create

        public ActionResult Create()
        {
            PopulateMakesDropDownList();
            return View();
        }

        //
        // POST: /PersonalComputer/Create

        [HttpPost]
        public ActionResult Create(PersonalComputer personalcomputer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PersonalComputers.Add(personalcomputer);
                    db.SaveChanges();
                    WriteToLog("Added" + "\t#" + personalcomputer.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }
            PopulateMakesDropDownList(personalcomputer.MakeID);
            return View(personalcomputer);
        }

        //
        // GET: /PersonalComputer/Edit/5

        public ActionResult Edit(int id)
        {
            PersonalComputer personalcomputer = db.PersonalComputers.Find(id);
            PopulateMakesDropDownList(personalcomputer.MakeID);
            return View(personalcomputer);
        }

        //
        // POST: /PersonalComputer/Edit/5

        [HttpPost]
        public ActionResult Edit(PersonalComputer personalcomputer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(personalcomputer).State = EntityState.Modified;
                    db.SaveChanges();
                    WriteToLog("Updated" + "\t#" + personalcomputer.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }
            PopulateMakesDropDownList(personalcomputer.MakeID);
            return View(personalcomputer);
        }

        //
        // GET: /PersonalComputer/Delete/5

        public ActionResult Delete(int id)
        {
            PersonalComputer personalcomputer = db.PersonalComputers.Find(id);
            return View(personalcomputer);
        }

        //
        // POST: /PersonalComputer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalComputer personalcomputer = db.PersonalComputers.Find(id);
            db.PersonalComputers.Remove(personalcomputer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}