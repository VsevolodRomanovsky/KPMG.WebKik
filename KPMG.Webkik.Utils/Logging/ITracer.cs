using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace KPMG.Webkik.Utils.Logging
{
    public interface ITracer
    {
        void TraceMethodIn(LogLevel level, string className, string methodName, IParameterCollection arguments);

        void TraceMethodReturn(LogLevel level, string className, string methodName, object returnValue, IParameterCollection outputs);

        void TraceMethodExpection(LogLevel level, string className, string methodName, Exception exception);

        void TraceMethodIn(LogLevel level, string className, string actionName, IDictionary<string, object> actionParameters);

        void TraceMethodIn(LogLevel level, string className, string methodName, IParameterCollection arguments, string messageFormat, object arg0);

        string FormatArgument(object value);
    }
}
