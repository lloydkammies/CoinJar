using System.Web.Mvc;
using CoinBase.Managers;
using CoinBase.Models;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace CoinJar
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<ICoinJar, CoinBase.Managers.CoinJar>();
            container.RegisterType<ICoin, Coin>("Store");

            return container;
        }
    }
}