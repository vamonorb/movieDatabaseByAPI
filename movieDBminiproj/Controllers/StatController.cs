using System;
using movieDBminiproj.Models;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movieDBminiproj.Controllers
{
    public class StatController : Controller
    {
        Entities db = new Entities();
        // GET: Stat
        public ActionResult Index()
        {
            int currentUser = (int)Session["userId"];

            StatModel model = new StatModel();
            var watchTime = db.Watchlist.Where(m => m.UserID == currentUser).Sum(m => (int?)m.Runtime) ?? 0;
            var watchedTime = db.Watched.Where(m => m.UserId == currentUser).Sum(m => m.Runtime);
            model.WatchStat = watchTime;
            model.WatchedStat = watchedTime;
            return View(model);
        }
    }
}