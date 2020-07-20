using CreatingWebAPIService.DataAcess;
using CreatingWebAPIService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CreatingWebAPIService.Controllers
{
    public class HomeController : ApiController
    {
        [HttpPost]
        public bool AddEmpDetails()
        {
            return true;
            //write insert logic

        }

        private string StatusData(string Status,string ReultData, IPRating RatingIP)
        {
            RatingIP.Status = Status;
            RatingIP.ReultData = ReultData;


            var dssd = new JavaScriptSerializer().Serialize(RatingIP);

            return dssd;
        }

       
        [HttpGet]
        [Route("api/Beer/AddBeerInfo")]
        [CustomFilterAttribute]
        public string AddRatingBeer(IPRating RatingIP)
        {
            if(RatingIP.Id==0)
            {
                return StatusData("Faild", "Invalid Beer ID", RatingIP);

            }
            if(!(RatingIP.Rating>0 && RatingIP.Rating<6))
            {
                
                return StatusData("Faild", "Invalid  Rating  it should be 1-5", RatingIP);

            }


            DataManupulation dfdsfsd = new DataManupulation();
            string vgd = "https://api.punkapi.com/v2/beers/" + RatingIP.Id;
            var BeerData = dfdsfsd.start_get(vgd);
            if (BeerData.Status)
            {

                RatingIP.Status = "Sucess";
                RatingIP.BeeName = BeerData.BeetInfoData.name;
                dfdsfsd.AddBeerRating(RatingIP);
                var json = new JavaScriptSerializer().Serialize(RatingIP);
                return json;
            }
            else
            {
                return StatusData("Faild", "Invalid Beer ID", RatingIP);
            }


        }

        [HttpGet]
        [Route("api/Beer/GetByName")]
        public string listofbeers([FromBody]JObject BeerNa)
        {
            string OutPutVVar = string.Empty;

            string BeerName = BeerNa["BeerName"].ToString();
            
            BeerName = BeerName.ToLower().Trim();
            if (BeerName =="")
            {
                return "Invalid Beer name";

            }
            DataManupulation dfdsfsd = new DataManupulation();
            IList<BeerInfo> sdsadasd = dfdsfsd.GetAllLList("https://api.punkapi.com/v2/beers", BeerName);
            if(sdsadasd!=null && sdsadasd.Count>0)
            {
              int idBeer=  sdsadasd[0].id;
              var beerddata=dfdsfsd.GetUserDetails(idBeer);

                BeerInfo sadasd = new BeerInfo();
                sadasd.id = idBeer;

                sadasd.name = sdsadasd[0].name;
                sadasd.description = sdsadasd[0].description;
                sadasd.userRatings = beerddata;
                var dssd = new JavaScriptSerializer().Serialize(sadasd);

                return dssd;
            }
            else
            {
                return "Invalid Beer name";

            }


        }


        [HttpGet]
        [Route("api/Beer/GetById/{id}")]
        public string listofbeersByID(int id)
        {
            if (id == 0)
            {
                return "Invalid Beer ID";

            }


            DataManupulation dfdsfsd = new DataManupulation();
            string vgd = "https://api.punkapi.com/v2/beers/" + id;
            var BeerData = dfdsfsd.start_get(vgd);
            if (BeerData.Status)
            {
                var beerddata = dfdsfsd.GetUserDetails(id);

                BeerInfo sadasd = new BeerInfo();
                sadasd.id = id;
                sadasd.name =  BeerData.BeetInfoData.name;
                sadasd.description = BeerData.BeetInfoData.description;
                sadasd.userRatings = beerddata;
                var dssd = new JavaScriptSerializer().Serialize(sadasd);

                return dssd;
            }
            else
            {
                return "Invalid Beer ID";
            }

           

        }
    }

}

