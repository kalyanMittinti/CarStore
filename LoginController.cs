using CarStore.ADO;
using CarStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarStore.Controllers
{
    public class LoginController : Controller
    {
        private Logindemo Logindemo;
        public LoginController()
        {
            Logindemo = new Logindemo();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginPage login)
        {

            if (ModelState.IsValid)
            {
               // bool isLogindemo = Logindemo.ValidateLogin(login.username, login.password);
                if (ValidateLogin(login.username,login.password))
                {
                    return RedirectToAction("Loginsucessfull", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(login);
        }
        private bool ValidateLogin(string username, string password)
        {
            return Logindemo.ValidateLogin(username, password);
        }

        public ActionResult Loginsucessfull()
        {
            return View();
        }
        public ActionResult logout()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            signOutUser();
            TempData["Message"] = "Logout successful";
            return RedirectToAction("logout", "Login"); // Redirect to home page after logout
        }
        private void signOutUser()
        {
            FormsAuthentication.SignOut();
        }
      
    }
}