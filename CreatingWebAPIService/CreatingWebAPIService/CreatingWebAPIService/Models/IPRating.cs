using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatingWebAPIService.Models
{
    public class IPRating
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }

        public string Comments { get; set; }

        public string BeeName { get; set; }
        public string Status { get; set; }
        public string ReultData { get; set; }

    }

    public  class FinalDisplay
    {

        public bool Status { get; set; }
        public BeerInfo BeetInfoData { get; set; }
    }


    public class BeerInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        
        public IList<RAatingInfo> userRatings { get; set; }
    }
    public class RAatingInfo
    {
        public string username { get; set; }
        public string rating { get; set; }
        public string comments { get; set; }
    }
}