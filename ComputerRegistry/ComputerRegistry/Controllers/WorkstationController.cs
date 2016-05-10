using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ComputerRegistry.Models;

namespace ComputerRegistry.Controllers
{
    public class WorkstationController : BaseController
    {
        private ComputerRegistryContext db = new ComputerRegistryContext();

        //
        // GET: /Workstation/

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

            var workstations = from pc in db.Workstations.Include(p => p.Make) select pc;

            if (!String.IsNullOrEmpty(searchString))
            {
                workstations = workstations.Where(c => c.Model.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(makeid))
            {
                int make = int.Parse(makeid);
                workstations = workstations.Where(c => c.MakeID == make);
            }

            if (workstations.Count() == 0)
                ViewBag.Message = "No records found";

            return View(workstations.ToList());
        }

        //
        // GET: /Workstation/Details/5

        public ViewResult Details(int id)
        {
            Workstation workstation = db.Workstations.Find(id);
            return View(workstation);
        }

        //
        // GET: /Workstation/Create

        public ActionResult Create()
        {
            PopulateMakesDropDownList();
            return View();
        }

        //
        // POST: /Workstation/Create

        [HttpPost]
        public ActionResult Create(Workstation workstation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Workstations.Add(workstation);
                    db.SaveChanges();
                    WriteToLog("Added" + "\t#" + workstation.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }
            PopulateMakesDropDownList(workstation.MakeID);
            return View(workstation);
        }

        //
        // GET: /Workstation/Edit/5

        public ActionResult Edit(int id)
        {
            Workstation workstation = db.Workstations.Find(id);
            PopulateMakesDropDownList(workstation.MakeID);
            return View(workstation);
        }

        //
        // POST: /Workstation/Edit/5

        [HttpPost]
        public ActionResult Edit(Workstation workstation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(workstation).State = EntityState.Modified;
                    db.SaveChanges();
                    WriteToLog("Updated" + "\t#" + workstation.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }
            PopulateMakesDropDownList(workstation.MakeID);
            return View(workstation);
        }

        //
        // GET: /Workstation/Delete/5

        public ActionResult Delete(int id)
        {
            Workstation workstation = db.Workstations.Find(id);
            return View(workstation);
        }

        //
        // POST: /Workstation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Workstation workstation = db.Workstations.Find(id);
            db.Workstations.Remove(workstation);
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