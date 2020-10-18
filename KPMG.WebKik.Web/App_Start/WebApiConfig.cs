using System.Web.Http;
using Microsoft.Practices.Unity;
using KPMG.WebKik.Web.App_Start;
using System.Web.Http.ExceptionHandling;
using Elmah.Contrib.WebApi;

namespace KPMG.WebKik.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, IUnityContainer container)
        {
            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(new GlobalExceptionFilterAttribute());
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            config.EnsureInitialized();

            config.Services.Add(typeof(IExceptionLogger), new ElmahExceptionLogger());
        }
    }
}
