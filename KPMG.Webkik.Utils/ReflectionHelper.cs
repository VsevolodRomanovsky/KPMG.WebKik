using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KPMG.Webkik.Utils
{
    public class ReflectionHelper
    {
        public static IEnumerable<Type> GetTypeByInterface<TInterface>(Assembly[] assemblies)
        {
            var type = typeof(TInterface);
            return  assemblies.SelectMany(x => x.GetTypes()).Where(x => type.IsAssignableFrom(x) && !x.IsInterface);
        }

        public static IEnumerable<Type> GeSubclassesOfType<T>(Assembly[] assemblies)
        {
            var type = typeof(T);
            return assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(type));
        }
    }
}
