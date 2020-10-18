using System;
using System.Net.NetworkInformation;

namespace KPMG.WebKik.DocumentProcessing.Helpers
{
    public static class ValueConvertHelper
    {
        public static DateTime TryGetDate(string dateString)
        {
            DateTime date;
            return DateTime.TryParse(dateString, out date) ? new DateTime(1900, 1, 1) : Convert.ToDateTime(dateString);
        }

        public static int TryGetInt(string value, int defaultValue)
        {
            int i;
            return int.TryParse(value, out i) ? defaultValue : Convert.ToInt32(value);
        }


        public static T ChangeType<T>(this object obj, T defaulVal)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
