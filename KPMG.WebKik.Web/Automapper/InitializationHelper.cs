using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;

namespace KPMG.WebKik.Web.Automapper
{
    public static class InitializationHelper
    {
        public static void Initialize(Assembly assembly)
        {
            InitializeAutommaper(assembly);
        }

        private static void InitializeAutommaper(Assembly assembly)
        {
            var methods = assembly.GetExportedTypes()
              .Where(t => t.IsClass)
              .SelectMany(t =>
                  t.GetMethods()
                   .Where(m => m.IsStatic && m.GetCustomAttribute<AutomapperInitializationAttribute>() != null));

            var cfg = new MapperConfigurationExpression();
            foreach (var method in methods)
            {
                try
                {
                    method.Invoke(null, new object[] { cfg });
                }
                catch (Exception exception)
                {
                    throw new Exception($"There is an error occured invoking {method.Name}. See InnerException for details.", exception);
                }
            }

            Mapper.Initialize(cfg);

#if DEBUG
            Mapper.AssertConfigurationIsValid();
#endif
        }

        public static void Initialize()
        {
            Initialize(Assembly.GetCallingAssembly());
        }
    }
}
