using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movieDBminiproj.Models
{
    public class WatchListViewModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string ImgPath { get; set; }
        public string Title { get; set; }
        public int ReleaseDate { get; set; }
        public int Runtime { get; set; }
    }

}