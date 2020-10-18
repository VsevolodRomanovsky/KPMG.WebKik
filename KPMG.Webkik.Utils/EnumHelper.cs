using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KPMG.Webkik.Utils
{
    public static class EnumHelper
    {
        public static IEnumerable<KeyValuePair<EnumType, string>> ToKeyValuePairs<EnumType>() where EnumType : struct, IConvertible
        {
            var type = typeof(EnumType);
            return Enum
                .GetValues(type)
                .Cast<EnumType>()
                .Select(value => new KeyValuePair<EnumType, string>(value, value.GetDescription()));
        }

        public static string GetDescription<EnumType>(this EnumType value) where EnumType : struct, IConvertible
        {
            var type = typeof(EnumType);
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return string.Empty;
        }
    }
}
