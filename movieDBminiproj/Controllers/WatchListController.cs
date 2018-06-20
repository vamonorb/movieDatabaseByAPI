using System;
using movieDBminiproj.Models;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace movieDBminiproj.Controllers
{
    public class WatchListController : Controller
    {
        Entities db = new Entities();

        // GET: WatchList
        public ActionResult Index()
        {
           WatchListViewModel model = new WatchListViewModel();
           int currentUser = (int)Session["userId"];

           var movieList = db.Watchlist.Where(u => u.UserID == currentUser).ToList();

            return View(movieList.ToList());

        }
       
        //Save a movie to watchlist
        public async Task<ActionResult> SaveToWatchList(int id)
        {
            MovieDetailViewModel movie = new MovieDetailViewModel();
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"movie/{id}?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();
                    movie = JsonConvert.DeserializeObject<MovieDetailViewModel>(jsonFile);
                }
            }
            int actualUser = (int)Session["userId"];
            var myMovieList = db.Watchlist.Where(m => m.UserID == actualUser).Select(m => m.MovieId).ToList();
            Watchlist myMovie = new Watchlist();

            if (!myMovieList.Contains(movie.Id))
            {
                myMovie.MovieId = movie.Id;
                myMovie.ImgPath = movie.Poster_path;
                myMovie.UserID = (int)Session["userId"];
                myMovie.Title = movie.Title;
                myMovie.ReleaseDate = movie.Release_date;
                myMovie.Runtime = movie.Runtime;
                db.Watchlist.Add(myMovie);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Watchlist", new { search = movie.Id });
        }

        //save a movie to watchedlist an if exist in the watchList remove it from there
        public async Task<ActionResult> SaveToWatchedList(int id)
        {
            MovieDetailViewModel movie = new MovieDetailViewModel();
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"movie/{id}?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();

                    movie = JsonConvert.DeserializeObject<MovieDetailViewModel>(jsonFile);
                }
            }
            int actualUser = (int)Session["userId"];
            Watched myWatchedMovie = new Watched();
            Watchlist myOldMovie = new Watchlist();
            var myMovieList = db.Watched.Where(m => m.UserId == actualUser).Select(m => m.MovieId).ToList();
            var myOldMovieList = db.Watchlist.Where(m => m.UserID == actualUser).Select(m => m.MovieId).ToList();

            if (!myMovieList.Contains(movie.Id))
            {
                myWatchedMovie.MovieId = movie.Id;
                myWatchedMovie.ImgPath = movie.Poster_path;
                myWatchedMovie.UserId = actualUser;
                myWatchedMovie.Title = movie.Title;
                myWatchedMovie.ReleaseDate = movie.Release_date;
                myWatchedMovie.Runtime = movie.Runtime;
                db.Watched.Add(myWatchedMovie);
                db.SaveChanges();
            }
            if (myOldMovieList.Contains(movie.Id))
            {
                myOldMovie = db.Watchlist.Where(m => m.MovieId == movie.Id).FirstOrDefault();
                db.Watchlist.Remove(myOldMovie);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Watchlist", new { search = movie.Id });
        }

        // GET: WatchList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WatchList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WatchList/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WatchList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WatchList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WatchList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WatchList/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
