using System.Diagnostics;

namespace KPMG.Webkik.Utils.Logging
{
    [DebuggerStepThrough]
    public sealed class DiagnosticsLog : ILog
    {
        private TraceSource traceSource;

        private static TraceEventType ConvertLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return TraceEventType.Verbose;
                case LogLevel.Info:
                    return TraceEventType.Information;
                case LogLevel.Warning:
                    return TraceEventType.Warning;
                case LogLevel.Error:
                    return TraceEventType.Error;
                default:
                    return TraceEventType.Warning;
            }
        }

        public DiagnosticsLog(string source)
        {
            traceSource = new TraceSource(source);
        }

        public DiagnosticsLog(XmlLoggerSettings traceSettings)             
        {
            traceSource = new TraceSource(traceSettings.SourceName, traceSettings.LogLevel);
            traceSource.Switch = new SourceSwitch(traceSettings.SourceName, traceSettings.LogLevel.ToString());
            traceSource.Switch.Level = traceSettings.LogLevel;
            traceSource.Listeners.Add(new XmlWriterTraceListener(traceSettings.FilePath));
            Trace.AutoFlush = true;
        }

        public void Write(LogLevel level, string message, string scope)
        {
            traceSource.TraceEvent(ConvertLogLevel(level), 0, string.Format("{0}    {1}", scope, message));
            //traceSource.TraceEvent(ConvertLogLevel(level), 0, $"{scope}    {message}");
        }

        public bool IsLogLevelEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return (traceSource.Switch.Level & SourceLevels.Verbose) == SourceLevels.Verbose;
                case LogLevel.Info:
                    return (traceSource.Switch.Level & SourceLevels.Information) == SourceLevels.Information;
                case LogLevel.Warning:
                    return (traceSource.Switch.Level & SourceLevels.Warning) == SourceLevels.Warning;
                case LogLevel.Error:
                    return (traceSource.Switch.Level & SourceLevels.Error) == SourceLevels.Error;
                default:
                    return (traceSource.Switch.Level & SourceLevels.Warning) == SourceLevels.Warning;
            }
        }

        public void Dispose()
        {
            if (traceSource != null)
            {
                traceSource.Close();
                traceSource = null;
            }
        }
    }
}
