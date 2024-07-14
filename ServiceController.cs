using System.Web.Mvc;
using CarStore.Models;
using CarStore.ADO;

namespace CarStore.Controllers
{
    public class ServiceController : Controller
    {
        private CarRepository serviceRepository;

        public ServiceController()
        {
            string connectionString = "YourConnectionStringHere";
            serviceRepository = new CarRepository (connectionString);
        }


        // GET: Service/BookByPhone/5
        // GET: Service/BookByPhone/{id}
        public ActionResult BookByPhone(int? id)
        {
            if (id == null)
            {
            
                return RedirectToAction("Index", "Home");
            }

            // Retrieve the car from the repository based on the provided ID
            Car car = serviceRepository.GetCarById(id.Value);

            // Check if the car exists
            if (car == null)
            {
                // If the car doesn't exist, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // Pass the car object to the view for phone booking
            return View(car);
        }


        // POST: Service/BookByPhone/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookByPhone(Car car)
        {
            if (ModelState.IsValid)
            {
                // Save the booking details in the database
                // For simplicity, let's assume a basic confirmation message
                TempData["Message"] = "Car booked successfully. We will contact you shortly.";

                // Redirect to a thank you page or display a confirmation message
                return RedirectToAction("ThankYou", "Service");
            }

            // If the model state is not valid, return the view with the current car object
            return View(car);
        }

        public ActionResult ThankYou()
        {
            // Display a thank you message after booking
            return View();
        }
    }
}
