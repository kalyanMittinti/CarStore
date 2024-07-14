using System;
using System.Data;
using System.Web.Mvc;
using CarStore.Models;
using CarStore.ADO;
using System.Linq;

namespace CarStore.Controllers
{
    public class CarController : Controller
    {
        private CarRepository carRepository;

        public CarController()
        {
            string connectionString = "Data Source=LAPTOP-LI6G9VON;Initial Catalog=login;User ID=sa;Password=123";
            carRepository = new CarRepository(connectionString);
        }

        // GET: Car
        public ActionResult Index()
        {
            DataTable cars = carRepository.GetAllCars();
            return View(cars);
        }
        // GET: /Car
        public ActionResult Indexs(string searchString)
        {
            // Retrieve all cars from the repository
            var dataTable = carRepository.GetAllCars();

            // Convert DataTable rows to list of Car objects
            var cars = dataTable.AsEnumerable().Select(row => new Car
            {
                Make = row.Field<string>("make"),
                Model = row.Field<string>("model"),
                Year = row.Field<int>("year"),
                Price = row.Field<decimal>("price"),
                Mileage = row.Field<int>("mileage")
            }).ToList();

            // Filter cars based on the search string
            if (!string.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c =>
                    c.Make.Contains(searchString) ||
                    c.Model.Contains(searchString) ||
                    c.Year.ToString().Contains(searchString) ||
                    c.Price.ToString().Contains(searchString) ||
                    c.Mileage.ToString().Contains(searchString)
                ).ToList();
            }

            // Pass the filtered cars to the view
            return View(cars);
        }


        public ActionResult IndexsWithSorting(string searchString, string sortOrder)
        {
            // Retrieve all cars from the repository
            var dataTable = carRepository.GetAllCars();

            // Convert DataTable rows to list of Car objects
            var cars = dataTable.AsEnumerable().Select(row => new Car
            {
                Make = row.Field<string>("make"),
                Model = row.Field<string>("model"),
                Year = row.Field<int>("year"),
                Price = row.Field<decimal>("price"),
                Mileage = row.Field<int>("mileage")
            }).ToList();

            // Filter cars based on the search string
            if (!string.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c =>
                    c.Make.Contains(searchString) ||
                    c.Model.Contains(searchString) ||
                    c.Year.ToString().Contains(searchString) ||
                    c.Price.ToString().Contains(searchString) ||
                    c.Mileage.ToString().Contains(searchString)
                ).ToList();
            }

            // Sorting
            switch (sortOrder)
            {
                case "Make":
                    cars = cars.OrderBy(c => c.Make).ToList();
                    break;
                case "Model":
                    cars = cars.OrderBy(c => c.Model).ToList();
                    break;
                case "Year":
                    cars = cars.OrderBy(c => c.Year).ToList();
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.Price).ToList();
                    break;
                case "Mileage":
                    cars = cars.OrderBy(c => c.Mileage).ToList();
                    break;
                default:
                    break;
            }

            // Pass the filtered and sorted cars to the view
            return View(cars);
        }
        


        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            Car car = carRepository.GetCarById(id);
            return View(car);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                carRepository.InsertCar(car);
                return RedirectToAction("Index");
            }

            return View(car);
        }
        public ActionResult Edit(int id)
        {
            // Retrieve the car from the repository based on the provided ID
            Car car = carRepository.GetCarById(id);

            // Check if the car exists
            if (car == null)
            {
                // If the car doesn't exist, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // Pass the car object to the view for editing
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                // Update the car in the repository
                carRepository.UpdateCar(car);
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return the view with the current car object
            return View(car);
        }
        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the car from the repository based on the provided ID
            Car car = carRepository.GetCarById(id);

            // Check if the car exists
            if (car == null)
            {
                // If the car doesn't exist, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // Pass the car object to the view for confirmation
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Delete the car from the repository
            carRepository.DeleteCar(id);
            return RedirectToAction("Index");
        }
    }
}
