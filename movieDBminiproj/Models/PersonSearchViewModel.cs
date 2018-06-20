using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieDBminiproj.Models
{
    public class KnownFor
    {
        public string Poster_path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Release_date { get; set; }
        public string Original_title { get; set; }
        public List<object> Genre_ids { get; set; }
        public int Id { get; set; }
        public string Media_type { get; set; }
        public string Original_language { get; set; }
        public string Title { get; set; }
        public string Backdrop_path { get; set; }
        public double Popularity { get; set; }
        public int Vote_count { get; set; }
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public string First_air_date { get; set; }
        public List<string> Origin_country { get; set; }
        public string Name { get; set; }
        public string Original_name { get; set; }
    }
    public class Result
    {
        public string Profile_path { get; set; }
        public bool Adult { get; set; }
        public int Id { get; set; }
        public List<KnownFor> Known_for { get; set; }
        public string Name { get; set; }
        public double Popularity { get; set; }
    }

    public class PersonSearchViewModel
    {
        public List<Result> Results { get; set; }
    }

    public class ResponsePerson
    {
        public bool Adult { get; set; }
        public List<string> Also_known_as { get; set; }
        public string Biography { get; set; }
        public string Birthday { get; set; }
        public string Deathday { get; set; }
        public int Gender { get; set; }
        public string Homepage { get; set; }
        public int Id { get; set; }
        public string Imdb_id { get; set; }
        public string Name { get; set; }
        public string Place_of_birth { get; set; }
        public double Popularity { get; set; }
        public string Profile_path { get; set; }
    }
}

