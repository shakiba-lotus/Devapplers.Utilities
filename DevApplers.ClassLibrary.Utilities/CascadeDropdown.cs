using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class CascadeDropDown
    {
        /// <summary>
        /// Filter records of second table based on Primary Key value
        /// </summary>
        /// <typeparam name="T">Second Table</typeparam>
        /// <param name="id">PK Value</param>
        /// <param name="where">Name of the Foreign Key</param>
        /// <returns></returns>
        public static IList<T> GetSource<T>(int id, string fk) where T : class
        {
            var result = Db.Get<T>(r =>
                typeof(T).GetProperty(fk)?.GetValue(r)?.ToString() == id.ToString()).ToList();
            return result;
        }

        /// <summary>
        /// Convert selected items to a SelectList
        /// </summary>
        /// <typeparam name="T">Second Table</typeparam>
        /// <param name="items">selected items of second table</param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> ToCascade<T>(this IEnumerable<T> items,
            Func<T, string> textField, Func<T, string> valueField) where T : class
        {
            var list = items.Select(selector => new SelectListItem
            {
                Text = textField(selector).ToString(),
                Value = valueField(selector).ToString()
            });
            return list;
        }
    }
}
