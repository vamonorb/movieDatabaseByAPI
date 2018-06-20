using System;
using movieDBminiproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace movieDBminiproj.Controllers
{
    public class WatchedController : Controller
    {
        Entities db = new Entities();

        // GET: Watched

        public ActionResult Index()
        {
            WatchedList model = new WatchedList();
            int currentUser = (int)Session["userId"];

            var watchedMovies = db.Watched.Where(u => u.UserId == currentUser).ToList();

            return View(watchedMovies.ToList());

        }
    }
}