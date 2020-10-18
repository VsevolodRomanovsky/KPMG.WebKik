using System.IO;

namespace KPMG.WebKik.Web.Razor
{
    public class SpaLayoutModel
    {
        public SpaLayoutModel(string rootUrl)
        {
            RootUrl = rootUrl.EndsWith("/") ? rootUrl : rootUrl + "/";
        }

        public string RootUrl { get; private set; }

        public bool TestEnvironment { get; set; }

        public string Url(string relativeUrl)
        {
            return Path.Combine(RootUrl, relativeUrl);
        }

        public string VersionUrl(string relativeUrl)
        {
            return Path.Combine(RootUrl, relativeUrl) + "?v=" + SpaInfo.Version;
        }
    }
}
