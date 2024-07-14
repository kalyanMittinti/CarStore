using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarStore.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLocation(double latitude, double longitude)
        {
           
            return Json(new { success = true, message = "Location added successfully" });
        }
    }
}