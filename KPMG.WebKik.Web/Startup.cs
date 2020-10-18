using System.Web.Http;
using System.Web.OData.Extensions;
using KPMG.WebKik.Data;
using KPMG.WebKik.Web;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Razor;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace KPMG.WebKik.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            const string connectionName = "WebKikConnection";
            var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };
            var container = config.UseUnity(connectionName);
            WebApiConfig.Register(config, container);
            app.UseWebApi(config);

            config.EnableEnumPrefixFree(true);

            ConfigureJsonSerializer(config);

            InitializationHelper.Initialize(typeof(AutomapperInitializationAttribute).Assembly);

            WebKikDataContext.Initialize(connectionName);

            app.UseSpaRazor("index.cshtml");
        }

        private void ConfigureJsonSerializer(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffzzz";
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

    }
}
