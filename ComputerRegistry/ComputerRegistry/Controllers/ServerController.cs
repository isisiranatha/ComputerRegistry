using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ComputerRegistry.Models;

namespace ComputerRegistry.Controllers
{ 
    public class ServerController : BaseController
    {
        private ComputerRegistryContext db = new ComputerRegistryContext();

        //
        // GET: /Server/

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

            var servers = from s in db.Servers.Include(s => s.Make) select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                servers = servers.Where(s => s.Model.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(makeid))
            {
                int make = int.Parse(makeid);
                servers = servers.Where(s => s.MakeID == make);
            }

            if (servers.Count() == 0)
                ViewBag.Message = "No records found";

            return View(servers.ToList());
        }

        //
        // GET: /Server/Details/5

        public ViewResult Details(int id)
        {
            Server server = db.Servers.Find(id);
            return View(server);
        }

        //
        // GET: /Server/Create

        public ActionResult Create()
        {
            PopulateMakesDropDownList();
            return View();
        }

        //
        // POST: /Server/Create

        [HttpPost]
        public ActionResult Create(Server server)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Servers.Add(server);
                    db.SaveChanges();
                    WriteToLog("Added" + "\t#" + server.InternalID.ToString());
                    return RedirectToAction("Index");
                }

            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }


            PopulateMakesDropDownList(server.MakeID);
            return View(server);
        }

        //
        // GET: /Server/Edit/5

        public ActionResult Edit(int id)
        {
            Server server = db.Servers.Find(id);
            PopulateMakesDropDownList(server.MakeID);
            return View(server);
        }

        //
        // POST: /Server/Edit/5

        [HttpPost]
        public ActionResult Edit(Server server)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(server).State = EntityState.Modified;
                    db.SaveChanges();
                    WriteToLog("Updated" + "\t#" + server.InternalID.ToString());
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (add a variable name after DataException) 
                ModelState.AddModelError("", "Unable to save changes " + ex.InnerException.InnerException);
            }

            PopulateMakesDropDownList(server.MakeID);
            return View(server);
        }

        //
        // GET: /Server/Delete/5

        public ActionResult Delete(int id)
        {
            Server server = db.Servers.Find(id);
            return View(server);
        }

        //
        // POST: /Server/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Server server = db.Servers.Find(id);
            db.Servers.Remove(server);
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