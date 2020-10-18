using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;

namespace KPMG.Webkik.Utils.Logging
{
    [DebuggerStepThrough]
    public partial class DefaultTracer : ITracer
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        private static readonly List<KeyValuePair<Type, Func<object, string>>> _formateters = new List<KeyValuePair<Type, Func<object, string>>>();

        private readonly ILog logger;

        public DefaultTracer(ILog logger)
        {
            this.logger = logger;
        }

        public void TraceMethodIn(LogLevel level, string className, string methodName, IParameterCollection arguments)
        {
            if (!logger.IsLogLevelEnabled(level)) return;
            var preMethodMessage = String.Format("{0}.{1}({2})", className, methodName, FormatArguments(arguments));
            //var preMethodMessage = $"{className}.{methodName}({FormatArguments(arguments)})";
            logger.Write(level, preMethodMessage);
        }

        public void TraceMethodIn(LogLevel level, string className, string actionName, IDictionary<string, object> actionParameters)
        {
            if (!logger.IsLogLevelEnabled(level)) return;
            var preMethodMessage = String.Format("{0}.{1}({2})",className, actionName, FormatArguments(actionParameters));
            //var preMethodMessage = $"{className}.{actionName}({FormatArguments(actionParameters)})";
            logger.Write(level, preMethodMessage);
        }

        public void TraceMethodIn(LogLevel level, string className, string methodName, IParameterCollection arguments, string messageFormat, object arg0)
        {
            if (!logger.IsLogLevelEnabled(level)) return;
            var methodMessage = String.Format("{0}.{1}({2}); {3}", className, methodName, FormatArguments(arguments), string.Format(messageFormat, arg0));
            //var methodMessage = $"{className}.{methodName}({FormatArguments(arguments)}); {string.Format(messageFormat, arg0)}";
            logger.Write(level, methodMessage);
        }

        public void TraceMethodReturn(LogLevel level, string className, string methodName, object returnValue, IParameterCollection outputs)
        {
            if (!logger.IsLogLevelEnabled(level)) return;
            var postMethodMessage = String.Format("{0}.{1}() -> {2}", className, methodName, returnValue);
            //var postMethodMessage = $"{className}.{methodName}() -> {returnValue}";
            logger.Write(level, postMethodMessage);
        }

        public void TraceMethodExpection(LogLevel level, string className, string methodName, Exception exception)
        {
            if (!logger.IsLogLevelEnabled(level)) return;
            var exceptionMessage = String.Format("{0}.{1} throws {2}", className, methodName, exception);
            //var exceptionMessage = $"{className}.{methodName} throws {exception}";
            logger.Write(level, exceptionMessage);
        }

        public string FormatArgument(object value)
        {
            if (value == null)
                return "null";
            try
            {
                for (var i = 0; i < _formateters.Count; i++)
                {
                    var type = _formateters[i].Key;
                    if (type.IsInstanceOfType(value))
                    {
                        var formatter = _formateters[i];
                        return formatter.Value(value);
                    }
                }

                return JsonFormat(value);
            }
            catch (Exception ex)
            {
                logger.Write(LogLevel.Warning, String.Format("There is a problem occured while formatting value of type {0}, {1}", value.GetType(), ex));
                    //$"There is a problem occured while formatting value of type {value.GetType()}, {ex}");
                try
                {
                    return value.ToString();
                }
                catch (Exception exs)
                {
                    logger.Write(LogLevel.Warning, String.Format("There is a problem occured while formatting value of type {0}, {1}", value.GetType().ToString(), exs));
                    //logger.Write(LogLevel.Warning, $"There is a problem occured while formatting value of type {value.GetType()}.ToString(), {exs}");
                    return "undefined";
                }
            }
        }

        private static void AddFormater<T>(Func<T, string> formatter)
        {
            _formateters.Add(new KeyValuePair<Type, Func<object, string>>(typeof(T), o => formatter((T)o)));
        }

        private static string JsonFormat(object value)
        {
            using (var writer = new StringWriter())
            {
                Serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                Serializer.Serialize(writer, value);
                return writer.GetStringBuilder().ToString();
            }
        }

        private string FormatArguments(IParameterCollection arguments)
        {
            if (arguments == null || arguments.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            for (var i = 0; i < arguments.Count; i++)
                sb.AppendFormat("{0}: {1},", arguments.ParameterName(i), FormatArgument(arguments[i]));

            if (sb.Length != 0)
                sb.Length--;
            return sb.ToString();
        }

        private object FormatArguments(IDictionary<string, object> actionParameters)
        {
            if (actionParameters == null || actionParameters.Count == 0)
                return string.Empty;
            return string.Join(",", actionParameters.Select(kv => string.Format("{0}: {1}", kv.Key,FormatArgument(kv.Value))));
            //return string.Join(",", actionParameters.Select(kv => $"{kv.Key}: {FormatArgument(kv.Value)}"));
        }
    }

}
