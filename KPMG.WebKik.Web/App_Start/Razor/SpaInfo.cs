using System.Reflection;

namespace KPMG.WebKik.Web.Razor
{
    public static class SpaInfo
    {
        private static string _version;

        private static string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static string Version => _version ?? (_version = GetVersion());
    }
}
