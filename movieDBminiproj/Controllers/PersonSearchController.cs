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

namespace movieDBminiproj.Controllers
{
    public class PersonSearchController : Controller
    {
        // GET: PersonSearch
        public async Task<ActionResult> SearchPerson(string search = "hank")
        {


            RootObject movieCollect = new RootObject();
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"person/{search}/movie_credits?api_key=0ac277c3f170caa0df815398709d9bb2&language=hu-BR"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();

                    movieCollect = JsonConvert.DeserializeObject<RootObject>(jsonFile);
                }
            }
            return View("SearchMovie", "Search", movieCollect.Cast.ToList());
        }


    }
}
    
