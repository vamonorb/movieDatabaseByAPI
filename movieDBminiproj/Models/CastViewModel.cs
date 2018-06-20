using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieDBminiproj.Models
{
    public class CastViewModel
    {
        public int Id { get; set; }
        public string Poster_path { get; set; }
        public string Title { get; set; }
        public string Release_date { get; set; }
        public double Vote_average { get; set; }
    }

    public class RootObject
    {
        public List<CastViewModel> Cast { get; set; }
        public bool IsExist { get; set; }


    }
}