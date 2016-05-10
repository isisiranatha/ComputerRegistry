using System;
using System.Linq;
using System.Web.Mvc;
using ComputerRegistry.Models;

namespace ComputerRegistry.Controllers
{
    public class BaseController : Controller
    {
        private ComputerRegistryContext db = new ComputerRegistryContext();
        
        public void PopulateMakesDropDownList(object selectedMake = null)
        {
            var makesQuery = from d in db.Makes
                             orderby d.Description
                             select d;
            ViewBag.MakeID = new SelectList(makesQuery, "MakeID", "Description", selectedMake);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public void WriteToLog(string action)
        {
            var log = log4net.LogManager.GetLogger(typeof(BaseController));

            log.Info(DateTime.Now.ToString() + "\t" + Request.ServerVariables["REMOTE_ADDR"] + "\t" + action);
        }
    }
}
