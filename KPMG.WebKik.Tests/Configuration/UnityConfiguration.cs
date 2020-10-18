using System;
using KPMG.WebKik.Data;
using Microsoft.Practices.Unity;

namespace KPMG.WebKik.Tests.Configuration
{
    public static class UnityConfiguration
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<WebKikDataContext>();
        }
    }
}