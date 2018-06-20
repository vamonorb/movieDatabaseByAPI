using System;
using movieDBminiproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

namespace movieDBminiproj.Controllers
{
    public class LoginViewController : Controller
    {
        Entities db = new Entities();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //Post
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var userNameList = db.User.Select(u => u.Username);

            if (!(userNameList.Contains(model.Username)))
            {
                ModelState.AddModelError(nameof(model.Username), "Nem létező felhasználónév és/vagy jelszó");
                return View(model);
            }
            var hashedPass = db.User.Where(u => u.Username == model.Username).Select(u => u.Pass).FirstOrDefault();
            if (model.Pass == null || Crypto.SHA256(model.Pass) != hashedPass)
            {
                ModelState.AddModelError(nameof(hashedPass), "Nem megfelelő jelszó");
                return View(model);
            }

            Session["role"] = "member";
            Session["username"] = db.User.Where(u => u.Username == model.Username).Select(u => u.Username).FirstOrDefault();
            var userId = db.User.Where(u => u.Username == model.Username).Select(u => u.Id).FirstOrDefault();
            Session["userId"] = userId;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}