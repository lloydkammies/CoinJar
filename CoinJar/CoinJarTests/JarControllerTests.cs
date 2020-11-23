using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoinBase.Managers;
using CoinBase.Models;
using CoinJar.Controllers;
using CoinJar.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CoinJarTests
{
    [TestClass]
    public class JarControllerTests
    {
        private readonly Mock<ICoinJar> _mockCoinJar;
        private readonly Mock<CoinJarContext> _mockCoinJarContext;

        JarController controller;
        public JarControllerTests()
        {
            _mockCoinJar = new Mock<ICoinJar>();
            _mockCoinJarContext = new Mock<CoinJarContext>();
        }

        [TestMethod]
        public void JarIndex_Success()
        {
            controller = new JarController(_mockCoinJar.Object);

            var result = controller.Index();
            ViewResult resultViewResult = result as ViewResult;
            JarModel resultModel = resultViewResult.Model as JarModel;

            Assert.IsNotNull(result);
            Assert.IsTrue(resultViewResult.ViewName == "Index");
            Assert.IsTrue(resultModel.Coins.Count == 6);

        }
        [TestMethod]
        public void InsertCoin_Success()
        {
            controller = new JarController(_mockCoinJar.Object);

            _mockCoinJar.Setup(s => s.AddCoin(It.IsAny<ICoin>()));
            _mockCoinJar.Setup(s => s.OuncesLeft()).Returns(42);

            var result = controller.InsertCoin("1c");

            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            
            Assert.IsTrue(jsonResult.Data.ToString().Contains("Success"));


        }
        [TestMethod]
        public void InsertCoin_Fail()
        {
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);

            controller = new JarController(_mockCoinJar.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            }; 
       

            _mockCoinJar.Setup(s => s.AddCoin(It.IsAny<ICoin>()));
            _mockCoinJar.Setup(s => s.OuncesLeft()).Returns(42);
            controller.HttpContext.Response.StatusCode = 400;

            var result = controller.InsertCoin(null);

            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;

            Assert.IsTrue(jsonResult.Data.ToString().Contains("Error"));
            Assert.IsTrue(jsonResult.Data.ToString().Contains("Invalid coinValue"));

        }


    }
}
