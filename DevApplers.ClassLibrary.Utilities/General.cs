using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class General
    {
        public static void IfIsNotNull<T>(this object obj, Action<T> method)
        {
            if (obj != null)
            {
                method((T)obj);
            }
        }

        public static TQ IfIsNotNull<T, TQ>(this object obj, Func<T, TQ> method)
        {
            return obj != null ? method((T)obj) : default(TQ);
        }

        public static string ListToString(this List<string> listString)
        {
            var str = listString.Aggregate("", (current, item) => current + item + ", ");
            return str.TrimEnd(' ').TrimEnd(',');
        }

        public static string ListToStringPersian(this List<string> listString)
        {
            var str = listString.Aggregate("", (current, item) => current + item + "، ");
            return str.TrimEnd(' ').TrimEnd('،');
        }

        public static List<T> StringValuesToList<T>(string value, string where, char separator = ',') where T : class
        {
            if (value == null) return null;

            var values = value.Split(separator);
            return Db.Get<T>(r => values.Contains(typeof(T).GetProperty(where)?.GetValue(r).ToString()));
        }

        public static object[] MultiSelectListSelectedValues(string value, char separator = ',')
        {
            if (value == null) return null;

            var values = value.TrimStart(',').Split(separator);
            var selectedValues = new object[values.Length];
            for (var i = 0; i < selectedValues.Length; i++)
            {
                selectedValues[i] = int.Parse(values[i]);
            }

            return selectedValues;
        }

        public static string JsonArrayToObj(this string jArray)
        {
            if (!(jArray.Contains("[") && jArray.Contains("{")))
                return jArray;

            var serializer = new JavaScriptSerializer();
            var arr = serializer.DeserializeObject(jArray);
            var jsonstr = "{";
            foreach (Dictionary<string, object> item in (IEnumerable)arr)
            {
                jsonstr += string.Concat("\"",
                    item["name"] + "\":",
                    item["value"].ToString().Contains("[") ? "" : "\"",
                    item["value"].ToString().JsonArrayToObj(),
                    item["value"].ToString().Contains("[") ? "" : "\"",
                    ","
                );
            }

            return jsonstr.TrimEnd(',') + "}";
        }

        public static dynamic DeserializeJson(this string jArr)
        {
            var json = JsonArrayToObj(jArr);
            var serializer = new JavaScriptSerializer();
            return serializer.DeserializeObject(json);
        }

        public static object TryGetValue(this Dictionary<string, object> dic, string key)
        {
            dic.TryGetValue(key, out var value);
            return value;
        }

        public static string RandomString(int length, bool withChar)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (!withChar)
                chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest, string excludeList = "")
        {
            var sourceProps = source.GetType().GetProperties().Where(x => x.CanRead).ToList();
            var destProps = dest.GetType().GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name &&
                                       !excludeList.Split(',').Contains(sourceProp.Name)))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }

        public static int? NextItemId<T>(this IEnumerable<T> items, int currentItemId) where T : class
        {
            var next = items.SkipWhile(obj =>
                    // Find the current item by it's Id in the collection
                    typeof(T).GetProperty("Id")?.GetValue(obj, null).ToString() != currentItemId.ToString())
                // Skip current item
                .Skip(1)
                // Get the first item after current item in the collection.
                .FirstOrDefault();
            if (next == null)
                return null;
            return int.Parse(typeof(T).GetProperty("Id")?.GetValue(next, null).ToString());
        }

        public static void SetPersianCulture(PersianCulture persianCulture)
        {
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
    }
}