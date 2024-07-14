using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace CarStore.Controllers
{
    public class BarcodeController : Controller
    {
        // GET: Barcode
        public ActionResult ProcessScan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProcessScan(string scannedData)
        {
            // Process the scanned data here
            // For example, you can store it in the database or perform any other operation
            return Json(new { success = true, message = "Scan processed successfully" });
        }
      



    }
}