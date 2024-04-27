using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace DevApplers.ClassLibrary.Utilities
{
    public sealed class PersianCulture : CultureInfo
    {
        public override Calendar Calendar { get; }
        public override Calendar[] OptionalCalendars { get; }

        public PersianCulture()
            : this("fa-IR", true)
        {
        }
        public PersianCulture(string cultureName, bool useUserOverride)
            : base(cultureName, useUserOverride)
        {
            //Temporary Value for cal.
            Calendar = base.OptionalCalendars[0];

            //populating new list of optional calendars.
            var optionalCalendars = new List<Calendar>();
            optionalCalendars.AddRange(base.OptionalCalendars);
            optionalCalendars.Insert(0, new PersianCalendar());

            var formatType = typeof(DateTimeFormatInfo);
            var calendarType = typeof(Calendar);

            var propertyInfo = calendarType.GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldInfo = formatType.GetField("optionalCalendars", BindingFlags.Instance | BindingFlags.NonPublic);

            //populating new list of optional calendar ids
            var newOptionalCalendarIDs = new int[optionalCalendars.Count];
            for (var i = 0; i < newOptionalCalendarIDs.Length; i++)
                if (propertyInfo != null)
                    newOptionalCalendarIDs[i] = (int)propertyInfo.GetValue(optionalCalendars[i], null);

            if (fieldInfo != null) fieldInfo.SetValue(DateTimeFormat, newOptionalCalendarIDs);

            OptionalCalendars = optionalCalendars.ToArray();
            Calendar = OptionalCalendars[0];
            DateTimeFormat.Calendar = OptionalCalendars[0];

            DateTimeFormat.MonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.MonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };
            DateTimeFormat.AbbreviatedMonthGenitiveNames = new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", "" };

            DateTimeFormat.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.ShortestDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
            DateTimeFormat.DayNames = new[] { "یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            DateTimeFormat.FirstDayOfWeek = DayOfWeek.Saturday;

            DateTimeFormat.AMDesignator = ""/*"ق.ظ"*/;
            DateTimeFormat.PMDesignator = ""/*"ب.ظ"*/;

            //DateTimeFormat.LongDatePattern = "dddd، dd MMMM yyyy";
            //DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            //DateTimeFormat.YearMonthPattern = "MMMM yyyy";
            DateTimeFormat.FullDateTimePattern = "dddd، dd MMMM yyyy HH:mm:ss";

            DateTimeFormat.SetAllDateTimePatterns(new[] { "dddd، dd MMMM yyyy" }, 'D');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "yyyy/MM/dd" }, 'd');
            DateTimeFormat.SetAllDateTimePatterns(new[] { "MMMM yyyy" }, 'Y');

            //NumberFormatInfo numberFormatInfo = new CultureInfo("en-US", false).NumberFormat;
            NumberFormat.NumberDecimalSeparator = ".";
        }
    }
}
