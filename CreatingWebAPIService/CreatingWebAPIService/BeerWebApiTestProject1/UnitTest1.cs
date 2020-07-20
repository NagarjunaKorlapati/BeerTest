using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreatingWebAPIService.Controllers;
using CreatingWebAPIService.Models;
using System.Web.Script.Serialization;
using CreatingWebAPIService.DataAcess;
using System.Collections.Generic;

namespace BeerWebApiTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCaseForGetByID()
        {
            DataManupulation dfdsfsd = new DataManupulation();
            string vgd = "https://api.punkapi.com/v2/beers/1";
            var BeerData = dfdsfsd.start_get(vgd);


          

            Assert.AreEqual(BeerData.Status,true );
            Assert.AreNotEqual(BeerData.Status, false);

            Assert.AreEqual(BeerData.BeetInfoData.name, "Buzz");

            Assert.AreNotEqual(BeerData.BeetInfoData.name, "KING fisher");



        }

        [TestMethod]
        public void TestCaseForGetAllByName()
        {
            DataManupulation dfdsfsd = new DataManupulation();
            IList<BeerInfo> sdsadasd = dfdsfsd.GetAllLList("https://api.punkapi.com/v2/beers", "Trashy Blonde");



            Assert.AreEqual(sdsadasd.Count, 1);
            Assert.AreNotEqual(sdsadasd[0].id, 1);

            Assert.AreEqual(sdsadasd[0].name, "Trashy Blonde");

            Assert.AreNotEqual(sdsadasd[0].name, "KING fisher");

        }


       
    }
}
