using CoinBase.Managers;
using CoinBase.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoinJar.Controllers
{
    public class JarController : Controller
    {
        private readonly ICoinJar _coinJar;
        public JarController(ICoinJar coinJar)
        {
            _coinJar = coinJar;
        }

        public ActionResult Index()
        {
            return View("Index", new JarModel() { Coins = GetCoins() });
        }

        [HttpPost]
        public JsonResult InsertCoin(string coinValue)
        {
            if (coinValue == "" || coinValue == null)
            {
                Response.StatusCode = 400;
                return Json(new { status = "Error", data = new { error = "Invalid coinValue", message = "Insert a correct coin." } }, JsonRequestBehavior.AllowGet);
            }

            var ouncesLeft = _coinJar.OuncesLeft();
            if (ouncesLeft < GetCoins()[coinValue].Volume)
            {
                return Json(new { status = "Warning", data = new { error = "", Message = "Jar-Full" } }, JsonRequestBehavior.AllowGet);
            }

            _coinJar.AddCoin(GetCoins()[coinValue]);
            return Json(new { Result = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTotalAmount()
        {
            var data = _coinJar.GetTotalAmount();
            return Json(new { status = "Success", data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Reset()
        {
            _coinJar.Reset();
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public decimal CalculateVolumeFluidOunces(decimal diameter, decimal thickness)
        {
            var radius = decimal.Divide(diameter, 2);
            var area = decimal.Multiply((decimal)Math.PI, radius);
            var volumeInSIUnits = decimal.Multiply(area, thickness);
            var volumeInFluidOunces = decimal.Multiply(volumeInSIUnits, 33814);
            return volumeInFluidOunces;
        }

        private Dictionary<string, Coin> GetCoins()
        {
            return new Dictionary<string, Coin>
            {
                {
                    "1c",
                    new Coin
                    {
                        Amount = 0.01m,
                        Volume = CalculateVolumeFluidOunces(0.001905m, 0.000152m),
                    }
                },
                {
                    "5c",
                    new Coin
                    {
                        Amount = 0.05m,
                        Volume = CalculateVolumeFluidOunces(0.002121m, 0.000195m),
                    }
                },
                {
                    "10c",
                    new Coin
                    {
                        Amount = 0.10m,
                        Volume = CalculateVolumeFluidOunces(0.001791m, 0.000135m),
                    }
                },
                {
                    "25c",
                    new Coin
                    {
                        Amount = 0.25m,
                        Volume = CalculateVolumeFluidOunces(0.002426m, 0.00175m),
                    }
                },
                {
                    "50c",
                    new Coin
                    {
                        Amount = 0.50m,
                        Volume = CalculateVolumeFluidOunces(0.003061m, 0.000215m),
                    }
                },
                {
                    "$1",
                    new Coin
                    {
                        Amount = 1m,
                        Volume = CalculateVolumeFluidOunces(0.002650m, 0.000258m),
                    }
                }
            };
        }
    }
}