using System;
using System.IO;
using System.Threading.Tasks;
using KPMG.Webkik.Utils;
using Owin.Types;
using RazorEngine;
using RazorEngine.Templating;

namespace KPMG.WebKik.Web.Razor
{
    public class SpaRazorMiddleware
    {
        private readonly ITemplateKey key;

        private readonly bool testEnvironment;

        public SpaRazorMiddleware(string defaultview)
        {
            key = CreateRazorKey(defaultview);
            if (!AppSettings.TryGet("TestEnvironment", out testEnvironment))
                testEnvironment = false;
        }

        public async Task Handle(OwinRequest request, OwinResponse response, Func<Task> next)
        {
            if (request.Uri.AbsolutePath.ToLower().Contains("/elmah.axd"))
            {
                if (next != null) await next();
                return;
            }

            using (var sw = new StringWriter())
            {
                var model = new SpaLayoutModel(request.PathBase) { TestEnvironment = testEnvironment };
                Engine.Razor.RunCompile(key, sw, model.GetType(), model);
                response.ContentType = "text/html";
                var buffer = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                await response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        private static ITemplateKey CreateRazorKey(string defaultview)
        {
            var key = new NameOnlyTemplateKey("spaLayout", ResolveType.Global, null);
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            var template = File.ReadAllText(Path.Combine(rootPath, defaultview));
            Engine.Razor.AddTemplate(key, new LoadedTemplateSource(template));
            return key;
        }
    }
}
