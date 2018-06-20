using System;
using movieDBminiproj.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Data;
using Newtonsoft.Json.Linq;

namespace movieDBminiproj.Controllers
{
    public class CarouselUpcomingController : Controller
    {
        // GET: Upcoming
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> UpcomingMovies()
        {
            var baseAddress = new Uri("https://api.themoviedb.org/3/");
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                using (var response = await httpClient.GetAsync($"movie/upcoming?api_key=0ac277c3f170caa0df815398709d9bb2&language=en-US"))
                {
                    string jsonFile = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(jsonFile);
                    List<JToken> results = jsonObj["results"].Children().ToList();
                    IList<ResultsUp> resultsUps = new List<ResultsUp>();
                    foreach (JToken result in results)
                    {
                        ResultsUp resultsUp = result.ToObject<ResultsUp>();
                        resultsUps.Add(resultsUp);
                    }
                    return View(resultsUps);


                    //upcoming = JsonConvert.DeserializeObject<Results>(jsonFile);



                }
            }

        }
    }
}