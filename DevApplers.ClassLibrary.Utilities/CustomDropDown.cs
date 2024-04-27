using System;
using System.Linq;
using System.Web.Mvc;

namespace DevApplers.ClassLibrary.Utilities
{
    public class CustomDropDown
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public static class CustomDropDownMonth
    {
        public static SelectList GetMonthSelectList()
        {
            var customDropDownMonth = (from EnumMonth enumMonth in Enum.GetValues(typeof(EnumMonth))
                select new CustomDropDown
                {
                    Value = (int) enumMonth,
                    Text = enumMonth.ToString()
                }).ToList();
            //foreach (EnumMonth enumMonth in Enum.GetValues(typeof(EnumMonth)))
            //    customDropDownMonth.Add(new CustomDropDown
            //    {
            //        Value = (int)enumMonth,
            //        Text = enumMonth.ToString()
            //    });
            return new SelectList(customDropDownMonth, "Value", "Text"); ;
        }
    }

    public enum EnumMonth
    {
        فروردین = 1,
        اردیبهشت,
        خرداد,
        تیر,
        مرداد,
        شهریور,
        مهر,
        آبان,
        آذر,
        دی,
        بهمن,
        اسفند
    }
}