using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ResortAppStore.Services.ERPBackEnd.API.Reports.BL
{

    public class StimulsoftFunctions
    {
        private const string Category = "HijriDate";


        public static int DateToHijriMonth(DateTime value)
        {
            HijriCalendar hijri = new HijriCalendar();

            DateTime greg = value;
            int year = hijri.GetYear(greg);
            int month = hijri.GetMonth(greg);
            int day = hijri.GetDayOfMonth(greg);

            //return day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            return month;
            //return Convert.ToInt32(value);
        }



        public static int DateToHijriYear(DateTime date)
        {
            HijriCalendar hijri = new HijriCalendar();

            DateTime greg = date;
            int year = hijri.GetYear(greg);
            int month = hijri.GetMonth(greg);
            int day = hijri.GetDayOfMonth(greg);

            //return day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            return year;
        }

        public static int DateToHijriDay(DateTime date)
        {
            HijriCalendar hijri = new HijriCalendar();

            DateTime greg = date;
            int year = hijri.GetYear(greg);
            int month = hijri.GetMonth(greg);
            int day = hijri.GetDayOfMonth(greg);

            //return day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            return day;
        }
        public static string ConvertToHijriDate(DateTime gregorianDate)
        {
            var hijriCalendar = new HijriCalendar();
            int hijriYear = hijriCalendar.GetYear(gregorianDate);
            int hijriMonth = hijriCalendar.GetMonth(gregorianDate);
            int hijriDay = hijriCalendar.GetDayOfMonth(gregorianDate);

            return $"{hijriYear:D4}/{hijriMonth:D2}/{hijriDay:D2}";
        }
     


        public static string MyFunc(string value)
        {
            return value.ToUpper();
        }


        public static void RegisterFunctions()
        {
            var ParamNames = new string[1];
            var ParamTypes = new Type[1];
            var ParamDescriptions = new string[1];


            ParamNames[0] = "value";
            ParamDescriptions[0] = "Take DateTime Argument to Get Hijri";
            ParamTypes[0] = typeof(DateTime);



            Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "DateToHijriMonth", "Description", typeof(StimulsoftFunctions),
                                     typeof(int), "Return Equivilant Hijri Month", ParamTypes, ParamNames, ParamDescriptions);

            Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "DateToHijriYear", "Description", typeof(StimulsoftFunctions),
                                     typeof(int), "Return Equivilant Hijri Year", ParamTypes, ParamNames, ParamDescriptions);

            Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "DateToHijriDay", "Description", typeof(StimulsoftFunctions),
                                     typeof(int), "Return Equivilant Hijrir Day", ParamTypes, ParamNames, ParamDescriptions);
            Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "ConvertToHijriDate", "Description", typeof(StimulsoftFunctions),
                                  typeof(string), "Return Equivilant Hijrir Date", ParamTypes, ParamNames, ParamDescriptions);
            Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "getOrginalName", "Description", typeof(StimulsoftFunctions),
                                typeof(string), "Return Equivilant Hijrir Date", ParamTypes, ParamNames, ParamDescriptions);

            //var ParamNames2 = new string[1];
            //var ParamTypes2 = new Type[1];
            //var ParamDescriptions2 = new string[1];


            //ParamNames2[0] = "value";
            //ParamDescriptions2[0] = "Take DateTime Argument and Return Equivalant Hijri";
            //ParamTypes2[0] = typeof(string);

            //Stimulsoft.Report.Dictionary.StiFunctions.AddFunction(Category, "HijriDates", "MyFunc", "Description", typeof(StimulsoftFunctions),
            //                         typeof(string), "Return Description", ParamTypes2, ParamNames2, ParamDescriptions2);
        }
    }

}