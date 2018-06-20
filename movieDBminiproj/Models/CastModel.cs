using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieDBminiproj.Models
{
    public class CastModel
    {
        public bool IsExist { get; set; }
        public bool IsExistWatched { get; set; }
        public string Poster_path { get; set; }
        public string Title { get; set; }
        public string Character { get; set; }
        public string Release_date { get; set; }
        public double Vote_average { get; set; }
        public string Original_title { get; set; }
        public int Id { get; set; }

    }
}