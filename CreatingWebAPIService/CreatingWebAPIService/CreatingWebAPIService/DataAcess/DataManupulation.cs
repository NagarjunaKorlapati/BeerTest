using CreatingWebAPIService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CreatingWebAPIService.DataAcess
{
    public class DataManupulation
    {

        
            //@"C:\Users\sathish.chepuri\Desktop\CreatingWebAPIService\CreatingWebAPIService\DataAcess\DataBase.json";
        
           //Path.GetFullPath("/Database.json");
        public void AddBeerRating(IPRating RAting)
        {


            string jsonFile = System.Web.HttpContext.Current.Server.MapPath("~/DataAcess/DataBase.json");
           
            try
            {
                var json = File.ReadAllText(jsonFile);
                var jsonObj = JObject.Parse(json);
                var experienceArrary = jsonObj.GetValue("BeerRating") as JArray;
                var newCompany = (JObject)JToken.FromObject(RAting);
                experienceArrary.Add(newCompany);

                jsonObj["BeerRating"] = experienceArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, newJsonResult);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void UpdateBeerRating(IPRating RAting)
        {
            string jsonFile = System.Web.HttpContext.Current.Server.MapPath("~/DataAcess/DataBase.json");

            string json = File.ReadAllText(jsonFile);

            try
            {
                var jObject = JObject.Parse(json);
                JArray experiencesArrary = (JArray)jObject["BeerRating"];
                var Id = RAting.Id;

                if (Id > 0)
                {
                    

                    foreach (var BeeRInfo in experiencesArrary.Where(obj => obj["Id"].Value<int>() == Id))
                    {
                        BeeRInfo["Comments"] = !string.IsNullOrEmpty(RAting.Comments) ? RAting.Comments : "";
                        BeeRInfo["Rating"] = Convert.IsDBNull(RAting.Rating) ? 0 : Convert.ToInt32(RAting.Rating);
                        BeeRInfo["UserName"] = !string.IsNullOrEmpty(RAting.UserName) ? RAting.UserName : "";
                    }

                    jObject["BeerRating"] = experiencesArrary;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }
                {
                    AddBeerRating(RAting);

                }
                
            }
            catch (Exception ex)
            {

               
            }
        }


        public FinalDisplay start_get(string  DataURL)
        {
            FinalDisplay dataView =new FinalDisplay();
            BeerInfo BeerData = new BeerInfo();


            bool returnSTatus = false;
            try
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(DataURL));

                WebReq.Method = "GET";


                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                if (WebResp.StatusCode == HttpStatusCode.OK)
                {
                    string jsonString;
                    using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
                    {
                        StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                        jsonString = reader.ReadToEnd();

                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        var d = jss.Deserialize<dynamic>(jsonString);
                        var fgsdgds = d[0];
                        BeerData.id = fgsdgds["id"];
                        BeerData.name = fgsdgds["name"];
                        BeerData.description = fgsdgds["description"];
                   //     dynamic stuff = JsonConvert.DeserializeObject(jsonString);
                        //stuff[0].
                        // List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonString);
                        returnSTatus = true;
                    }

                    //   List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonString);

                   

                }

            }
            catch(Exception ex)
            {

            }
            dataView.Status = returnSTatus;
            dataView.BeetInfoData = BeerData;
            return dataView;
            
        }



        public IList<BeerInfo> GetAllLList(string DataURL,string Name)
        {

            IList<BeerInfo> LstBeerData = new List<BeerInfo>();


            
            try
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(DataURL));

                WebReq.Method = "GET";


                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                if (WebResp.StatusCode == HttpStatusCode.OK)
                {
                    string jsonString;
                    using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
                    {
                        StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                        jsonString = reader.ReadToEnd();

                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        var d = jss.Deserialize<dynamic>(jsonString);
                        
                        foreach(var dsad in d)
                        {
                            string dsfssdsdd = dsad["name"];
                            BeerInfo BeerData = new BeerInfo();
                            if (dsfssdsdd.ToLower().Trim() == Name.ToLower().Trim())
                            {
                                BeerData.id = dsad["id"];
                                BeerData.name = dsad["name"];
                                BeerData.description = dsad["description"];

                                LstBeerData.Add(BeerData);
                            }
                        }

                       
                      
                    }

                  



                }

            }
            catch (Exception ex)
            {
                LstBeerData = new List<BeerInfo>();

            }
           
            return LstBeerData;

        }

        //private void DeleteCompany(IPRating RAting)
        //{
        //    var json = File.ReadAllText(jsonFile);
        //    try
        //    {
        //        var jObject = JObject.Parse(json);
        //        JArray experiencesArrary = (JArray)jObject["experiences"];
        //        Console.Write("Enter Company ID to Delete Company : ");
        //        var companyId = Convert.ToInt32(Console.ReadLine());

        //        if (companyId > 0)
        //        {
        //            var companyName = string.Empty;
        //            var companyToDeleted = experiencesArrary.FirstOrDefault(obj => obj["companyid"].Value<int>() == companyId);

        //            experiencesArrary.Remove(companyToDeleted);

        //            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
        //            File.WriteAllText(jsonFile, output);
        //        }
        //        else
        //        {
        //            Console.Write("Invalid Company ID, Try Again!");
        //            UpdateCompany();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public IList<RAatingInfo> GetUserDetails(int id)
        {
            string jsonFile = System.Web.HttpContext.Current.Server.MapPath("~/DataAcess/DataBase.json");
            IList<RAatingInfo> lstRtn = new List<RAatingInfo>();
            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    
                    JArray experiencesArrary = (JArray)jObject["BeerRating"];
                    if (experiencesArrary != null)
                    {
                        foreach (var item in experiencesArrary)
                        {
                            int  IdData = Convert.IsDBNull(item["Id"]) ? 0 : Convert.ToInt32(item["Id"]);
                            if (IdData == id)
                            {
                                RAatingInfo Ratinginfo = new RAatingInfo();
                                Ratinginfo.username = Convert.IsDBNull(item["UserName"]) ? "" : Convert.ToString(item["UserName"]);
                                Ratinginfo.rating = Convert.IsDBNull(item["Rating"]) ? "" : Convert.ToString(item["Rating"]);
                                Ratinginfo.comments = Convert.IsDBNull(item["Comments"]) ? "" : Convert.ToString(item["Comments"]);
                                lstRtn.Add(Ratinginfo);
                            }

                           
                           
                        }

                    }
                   

                }
            }
            catch (Exception)
            {
                lstRtn = new List<RAatingInfo>();
            }
            return lstRtn;
        }

    }
}   
