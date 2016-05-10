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
    public class ServercomputerController : BaseController
    {
        private ComputerRegistryContext db = new ComputerRegistryContext();

        //
        // GET: /ServerComputer/

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

            var servercomputers = from s in db.ServerComputers.Include(s => s.Make) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                servercomputers = servercomputers.Where(s => s.Model.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(makeid))
            {
                int make = int.Parse(makeid);
                servercomputers = servercomputers.Where(s => s.MakeID == make);
            }

            if (servercomputers.Count() == 0)
                ViewBag.Message = "No records found";

            return View(servercomputers.ToList());
        }

        //
        // GET: /ServerComputer/Details/5

        public ViewResult Details(int id)
        {
            ServerComputer servercomputer = db.ServerComputers.Find(id);
            return View(servercomputer);
        }

        //
        // GET: /ServerComputer/Create

        public ActionResult Create()
        {
            PopulateMakesDropDownList();
            return View();
        }

        //
        // POST: /ServerComputer/Create

        [HttpPost]
        public ActionResult Create(ServerComputer servercomputer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ServerComputers.Add(servercomputer);
                    db.SaveChanges();
                    WriteToLog("Added" + "\t#" + servercomputer.InternalID.ToString());
                    return RedirectToAction("Index");
                }

            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }


            PopulateMakesDropDownList(servercomputer.MakeID);
            return View(servercomputer);
        }

        //
        // GET: /ServerComputer/Edit/5

        public ActionResult Edit(int id)
        {
            ServerComputer servercomputer = db.ServerComputers.Find(id);
            PopulateMakesDropDownList(servercomputer.MakeID);
            return View(servercomputer);
        }

        //
        // POST: /ServerComputer/Edit/5

        [HttpPost]
        public ActionResult Edit(ServerComputer servercomputer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(servercomputer).State = EntityState.Modified;
                    db.SaveChanges();
                    WriteToLog("Updated" + "\t#" + servercomputer.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }

            PopulateMakesDropDownList(servercomputer.MakeID);
            return View(servercomputer);
        }

        //
        // GET: /ServerComputer/Delete/5

        public ActionResult Delete(int id)
        {
            ServerComputer servercomputer = db.ServerComputers.Find(id);
            return View(servercomputer);
        }

        //
        // POST: /ServerComputer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ServerComputer servercomputer = db.ServerComputers.Find(id);
            db.ServerComputers.Remove(servercomputer);
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