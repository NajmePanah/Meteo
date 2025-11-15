using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Meteo.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static string ToEnglishNumber(this string str)
        {
            try
            {
                var fa = CultureInfo.GetCultureInfoByIetfLanguageTag("fa");
                var ar = CultureInfo.GetCultureInfoByIetfLanguageTag("ar");
                var en = CultureInfo.GetCultureInfoByIetfLanguageTag("en");
                if (!String.IsNullOrEmpty(str))
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        str = str.Replace(fa.NumberFormat.NativeDigits[i], en.NumberFormat.NativeDigits[i]);
                    }
                    for (int i = 0; i <= 9; i++)
                    {
                        str = str.Replace(ar.NumberFormat.NativeDigits[i], en.NumberFormat.NativeDigits[i]);
                    }
                }
            }
            catch { }
            return str;
        }
        public static DateTime? ToDateTime(this string persianDate)
        {
            if (persianDate != null)
            {
                if (persianDate.ToEnglishNumber().Split('/').Count() == 1)
                {
                    persianDate = persianDate + "/01/01";
                }
                else if (persianDate.ToEnglishNumber().Split('/').Count() == 2)
                {
                    persianDate = persianDate + "/01";
                }
                var dt = persianDate.ToEnglishNumber().Split('/');
                var year = int.Parse(dt[0]);
                if (year < 1300)
                {
                    if (year < 50)
                    {
                        year += 1400;
                    }
                    else
                    {
                        year += 1300;
                    }
                }
                var pdt = new PersianDateTime(year, int.Parse(dt[1]), int.Parse(dt[2]));
                return pdt.ToDateTime();
            }
            else
                return null;
        }
        public static DateTime? TryToDateTime(this string persianDate)
        {
            try
            {
                return persianDate.ToDateTime();
            }
            catch { }
            return null;

        }
        public static string ToPersianShortDateString(this DateTime dt)
        {
            
                PersianDateTime pdt = new PersianDateTime(dt);
                return pdt.ToShortDateString();
        }
        public static string ToPersianShortDateString(this DateOnly d)
        {
            var dt = d.ToDateTime(TimeOnly.MinValue); 
            PersianDateTime pdt = new PersianDateTime(dt);
            return pdt.ToShortDateString();
        }
        public static string ToPersianShortDateString(this DateOnly? d)
        {
            if (d != null)
            {
                return d.Value.ToPersianShortDateString();
            }
            return "";
        }
        public static string TryToPersianShortDateString(this DateTime? dt)
        {
            if (dt != null)
            {
                PersianDateTime pdt = new PersianDateTime(dt);
                return pdt.ToShortDateString();
            }
            return "";
        }
        public static DateTime? ConvertPersianToGregorian(this string persianDate)
        {
            try
            {
                // رشته ورودی را به اجزای سال، ماه و روز تقسیم می‌کنیم
                string[] dateParts = persianDate.Split('/');
                if (dateParts.Length != 3)
                    return null;

                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);

                // ایجاد یک تقویم شمسی
                PersianCalendar persianCalendar = new PersianCalendar();

                // تبدیل تاریخ شمسی به میلادی
                DateTime gregorianDate = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

                return gregorianDate;
            }
            catch (Exception ex)
            {
                //throw new Exception("Error converting Persian date to Gregorian: " + ex.Message);
                return null;
                Serilog.Log.Error(ex.Message);
            }
        }
        public static DateTime ToPersianDate(this DateTime dt)
        {
            var c = new DateTime();
            try
            {
                PersianCalendar pc = new PersianCalendar();
                int year = pc.GetYear(dt);
                int month = pc.GetMonth(dt);
                int day = pc.GetDayOfMonth(dt);
                int hour = pc.GetHour(dt);
                int min = pc.GetMinute(dt);
                c = new DateTime(year, month, day, hour, min, 0);
            }
            catch { }
            return c;
        }
        public static DateTime ToPersianDateTring(this DateTime dt)
        {
            var c = new DateTime();
            try
            {
                PersianCalendar pc = new PersianCalendar();
                int year = pc.GetYear(dt);
                int month = pc.GetMonth(dt);
                int day = pc.GetDayOfMonth(dt);
                int hour = pc.GetHour(dt);
                int min = pc.GetMinute(dt);
                c = new DateTime(year, month, day, hour, min, 0);
            }
            catch { }
            return c;
        }
        public static DateTime ToMiladiDate(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);

        }
        public static string ToPersianLongDateString(this DateTime dt)
        {
            PersianDateTime pdt = new PersianDateTime(dt);
            return pdt.ToLongDateString();
        }
        public static string ToPersianLongDateString(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                try
                {
                    PersianDateTime pdt = new PersianDateTime(dt);
                    return pdt.ToLongDateString();
                }
                catch { }
            }
            return "";
        }
        public static TimeSpan GetTime(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                try
                {
                    PersianDateTime pdt = new PersianDateTime(dt);
                    return dt.Value.TimeOfDay;
                }
                catch { }
            }
            return new TimeSpan(0);
        }
        public static string ToPersianShortDateString(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                try
                {
                    PersianDateTime pdt = new PersianDateTime(dt);
                    return pdt.ToShortDateString();
                }
                catch { }
            }
            return "";
        }
        public static string ToPersianLongDateTimeString(this DateTime dt)
        {
            PersianDateTime pdt = new PersianDateTime(dt);
            return pdt.ToLongDateTimeString();
        }
        public static string ToPersianShortDateTimeString(this DateTime dt)
        {
            PersianDateTime pdt = new PersianDateTime(dt);
            return pdt.ToShortDateString() + " " + pdt.ToLongTimeString();
        }
        public static string ToPersianShortDateTimeString(this DateTime? dt)
        {
            if (dt != null)
            {
                PersianDateTime pdt = new PersianDateTime(dt);
                return pdt.ToShortDateString() + " " + pdt.ToLongTimeString();
            }
            else
            {
                return "";
            }
        }
        public static string ToPersianDateTimeString(this DateTime? dt)
        {
            if (dt != null)
            {
                PersianDateTime pdt = new PersianDateTime(dt);
                return pdt.ToLongDateTimeString();
            }
            return "";
        }
        public static DateTime MakeNowIfNull(this DateTime dt)
        {
            if (dt == null)
            {
                return DateTime.Now;
            }
            return dt;
        }
        public static DateTime WeekStart(this DateTime dt)
        {
            return dt.AddDays(-(int)dt.DayOfWeek).Date;
        }
        public static DateTime WeekEnd(this DateTime dt)
        {
            return dt.WeekStart().AddDays(7).AddSeconds(-1).Date;
        }
        public static DateTime PersianWeekStart(this DateTime dt)
        {
            return dt.AddDays(-((int)dt.DayOfWeek) - 1).Date;
        }
        public static DateTime PersianWeekEnd(this DateTime dt)
        {
            return dt.PersianWeekStart().AddDays(7).AddSeconds(-1).Date;
        }
        public static DateTime PersianMonthStart(this DateTime dt)
        {
            return dt.AddDays(-dt.ToPersianDate().Day).Date;
        }
        public static DateTime PersianMonthEnd(this DateTime dt)
        {
            return dt.AddMonths(1).AddDays(-dt.AddMonths(1).ToPersianDate().Day).Date;
        }
        public static DateTime PersianYearStart(this DateTime dt)
        {
            return dt.AddDays(-dt.ToPersianDate().DayOfYear).Date;
        }
        public static DateTime PersianYearEnd(this DateTime dt)
        {
            return dt.AddYears(1).AddDays(-dt.AddYears(1).ToPersianDate().DayOfYear).AddSeconds(-1).Date;
        }
        public static string ToShamsiDayName(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                default:
                    return "نامشخص";
            }
        }
        public static List<SelectListItem> GetPersianMonths()
        {
            var months = new List<SelectListItem>();
            PersianCalendar persianCalendar = new PersianCalendar();

            for (int i = 1; i <= 12; i++)
            {
                string persianMonth = new DateTime(1400, i, 1, persianCalendar).ToString("MMMM", new CultureInfo("fa-IR"));
                months.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = persianMonth
                });
            }

            return months;
        }

        public static List<SelectListItem> GetNewPersianMonths()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "فروردین" },
                new SelectListItem { Value = "2", Text = "اردیبهشت" },
                new SelectListItem { Value = "3", Text = "خرداد" },
                new SelectListItem { Value = "4", Text = "تیر" },
                new SelectListItem { Value = "5", Text = "مرداد" },
                new SelectListItem { Value = "6", Text = "شهریور" },
                new SelectListItem { Value = "7", Text = "مهر" },
                new SelectListItem { Value = "8", Text = "آبان" },
                new SelectListItem { Value = "9", Text = "آذر" },
                new SelectListItem { Value = "10", Text = "دی" },
                new SelectListItem { Value = "11", Text = "بهمن" },
                new SelectListItem { Value = "12", Text = "اسفند" },
            };
        }
        public static List<SelectListItem> GetPersianYears(int startYear, int endYear)
        {
            var years = new List<SelectListItem>();
            for (int year = startYear; year <= endYear; year++)
            {
                years.Add(new SelectListItem
                {
                    Value = year.ToString(),
                    Text = year.ToString()
                });
            }
            return years;
        }
        public static int GetCurrentPersianYear()
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime now = DateTime.Now; // Get the current date  
            return persianCalendar.GetYear(now); // Convert to Persian year  
        }
        public static string TimeAgo(this DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
            {
                return "لحظاتی قبل";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                int minutes = (int)timeSpan.TotalMinutes;
                return $"{minutes} دقیقه قبل";
            }
            else if (timeSpan.TotalHours < 24)
            {
                int hours = (int)timeSpan.TotalHours;
                return $"{hours} ساعت قبل";
            }
            else if (timeSpan.TotalDays < 30)
            {
                int days = (int)timeSpan.TotalDays;
                return $"{days} روز قبل";
            }
            else if (timeSpan.TotalDays < 365)
            {
                int months = (int)(timeSpan.TotalDays / 30); // با احتساب میانگین 30 روز در هر ماه  
                return $"{months} ماه قبل";
            }
            else
            {
                int years = (int)(timeSpan.TotalDays / 365);
                return $"{years} سال قبل";
            }
        }
    }
}
