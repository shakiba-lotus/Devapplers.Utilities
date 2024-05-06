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

    public static class PersianMonths
    {
        public static SelectList GetSelectList()
        {
            var PersianMonthsDropDown = (from EnumMonth enumMonth in Enum.GetValues(typeof(EnumMonth))
                select new CustomDropDown
                {
                    Value = (int) enumMonth,
                    Text = enumMonth.ToString()
                }).ToList();
            return new SelectList(PersianMonthsDropDown, "Value", "Text");
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