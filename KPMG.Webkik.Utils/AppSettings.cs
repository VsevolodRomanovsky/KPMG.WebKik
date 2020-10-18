using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KPMG.Webkik.Utils
{
    public static class AppSettings
    {
        public static bool TryGet<T>(string key, out T value)
        {
            var stringvalue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(stringvalue))
            {
                value = default(T);
                return false;
            }

            value = (T)Convert.ChangeType(stringvalue, typeof(T));
            return true;
        }

        public static T Get<T>(string key)
        {
            T value;
            if (!TryGet(key, out value))
                throw new Exception(String.Format("appSettings '{0}' must be specified in the config file", key));
                //throw new Exception($"appSettings '{key}' must be specified in the config file");
            return value;
        }

        public static TimeSpan GetTimeSpan(string key)
        {
            string value = Get<string>(key);
            return TimeSpan.Parse(value);
        }

        public static string GetConnectionString(string name)
        {
            var c = ConfigurationManager.ConnectionStrings[name];
            if (c == null)
                throw new Exception(String.Format("Connection string with name = '{0}' must be specified in the config file", name));
            //throw new Exception($"Connection string with name = '{name}' must be specified in the config file");
            return c.ConnectionString;
        }

        public static IDictionary<string, string> GetDictionarySection(string sectionName)
        {
            var section = ConfigurationManager.GetSection(sectionName) as Hashtable;
            if (section == null)
                throw new Exception(String.Format("Couldn't find section {0} in the config file", sectionName));
            //throw new Exception($"Couldn't find section {sectionName} in the config file");
            return section.Cast<DictionaryEntry>().ToDictionary(d => (string)d.Key, d => (string)d.Value);
        }
    }

}
