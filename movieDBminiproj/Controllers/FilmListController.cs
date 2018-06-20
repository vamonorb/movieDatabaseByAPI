using System;
using movieDBminiproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace movieDBminiproj.Controllers
{
    //dynamic movieCollect = ;

    public class FilmListController : Controller
    {
        //ObservableCollection<CastViewModell> movieCollect = new ObservableCollection<CastViewModell>();
        public async Task<string> SearchMovie(string search)
        {
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"person/{search}/movie_credits?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();
                    return jsonFile;
                }
            }
        }

        [HttpGet]
        public ActionResult Details()
        {
            CastViewModel model = new CastViewModel();
            List <CastViewModel> movieList = new List<CastViewModel>();
            dynamic movieCollect = JsonConvert.DeserializeObject<RootObject>(SearchMovie("31").Result);
            foreach (var movie in movieCollect)
            {
                movieList.Add(movie);
            }
            
        
            return View(movieList);
        }
      
        
    }
}