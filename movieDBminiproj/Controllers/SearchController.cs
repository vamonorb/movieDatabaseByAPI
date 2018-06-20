using System;
using movieDBminiproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Newtonsoft.Json.Linq;

namespace movieDBminiproj.Controllers
{
    public class SearchController : Controller
    {
        Entities db = new Entities();
               public async Task<ActionResult> MovieByPerson(string search)
        {
            int currentUserId = 0;
            if (Session["userId"] != null)
            {
                currentUserId = (int)Session["userId"];
            }
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"person/{search}/movie_credits?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    var myMovies = db.Watchlist.Where(m => m.UserID == currentUserId).Select(m => m.MovieId).ToList();
                    var myMoviesWatched = db.Watched.Where(m => m.UserId == currentUserId).Select(m => m.MovieId).ToList();

                    string jsonFile = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(jsonFile);
                    List<JToken> casts = jsonObj["cast"].Children().ToList();
                    List<CastModel> movieResults = new List<CastModel>();
                    foreach (JToken item in casts)
                    {
                        CastModel model = item.ToObject<CastModel>();
                        model.IsExist = myMovies.Contains(model.Id);
                        model.IsExistWatched = myMoviesWatched.Contains(model.Id);
                        movieResults.Add(model);
                    }
                    return View(movieResults);
                }
            }
        }

        public async Task<ActionResult> MovieDetails(int search)
        {
            int currentUserId = 0;

            if (Session["userId"] != null)
            {
                 currentUserId = (int)Session["userId"];

            }
            MovieDetailViewModel movie = new MovieDetailViewModel();
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"movie/{search}?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();
                    movie = JsonConvert.DeserializeObject<MovieDetailViewModel>(jsonFile);
                }
            }
            var myMovies = db.Watchlist.Where(m => m.UserID == currentUserId).Select(m => m.MovieId).ToList();
            var myMoviesWatched = db.Watched.Where(m => m.UserId == currentUserId).Select(m => m.MovieId).ToList();
            movie.IsExistWatched = myMoviesWatched.Contains(movie.Id);
            movie.IsExist = myMovies.Contains(movie.Id);
            return View(movie);
        }

        public async Task<ActionResult> PersonSearch(string search)
        {
            PersonSearchViewModel persons = new PersonSearchViewModel();
            var baseAddress = new Uri("http://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"search/person?api_key=0ac277c3f170caa0df815398709d9bb2&query={search}&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();
                    persons = JsonConvert.DeserializeObject<PersonSearchViewModel>(jsonFile);
                }
            }
            return View(persons);
        }
    }

}