using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using movieDBminiproj.Models;
using System.Web.Helpers;

namespace movieDBminiproj.Controllers
{
    public class UserRegisterController : Controller
    {
        Entities db = new Entities();

        // GET: UserRegister
        public ActionResult Register()
        {
            return View();
        }

        //Post: create
        [HttpPost]
        public ActionResult Register(UserRegisterViewModel model)
        {
            try
            {
                var userNamelist = db.User.Select(u => u.Username).ToList();
                if (userNamelist.Contains(model.Username))
                    ModelState.AddModelError(nameof(model.Username), "Van már ilyen nevű felhasználó");
                if (model.Pass != model.PassConfirmed)
                    ModelState.AddModelError(nameof(model.PassConfirmed), "Nem egyezik a két jelszó");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var hashPass = Crypto.SHA256(model.Pass);
                User user = new User();
                user.Username = model.Username;
                user.Pass = hashPass;

                db.User.Add(user);
                db.SaveChanges();

                Session["role"] = "member";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}