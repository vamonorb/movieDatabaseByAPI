using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieDBminiproj.Models
{
    public class ResultsUp
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Poster_path { get; set; }
        public double VoteAverage { get; set; }
        public List<int> Genre_ids { get; set; }
        public string Release_date { get; set; }

        List<ResultsUp> Upcoming { get; set; }
    }

   }

