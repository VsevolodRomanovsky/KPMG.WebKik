using System;

namespace KPMG.Webkik.Utils.Logging
{
    public interface ILog : IDisposable
    {
        void Write(LogLevel level, string message, string scope = "");

        bool IsLogLevelEnabled(LogLevel level);
    }
}
