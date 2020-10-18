using System.Diagnostics;

namespace KPMG.Webkik.Utils.Logging
{
    public class XmlLoggerSettings
    {
        public SourceLevels LogLevel { get; set; }

        public string FilePath { get; set; }

        public string SourceName { get; set; }
    }
}
