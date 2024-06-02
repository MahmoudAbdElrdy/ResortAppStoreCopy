using System;
using System.Globalization;

public static class ReportCustomFunctions
{
    public static string ConvertToHijriDate(DateTime gregorianDate)
    {
        var hijriCalendar = new HijriCalendar();
        int hijriYear = hijriCalendar.GetYear(gregorianDate);
        int hijriMonth = hijriCalendar.GetMonth(gregorianDate);
        int hijriDay = hijriCalendar.GetDayOfMonth(gregorianDate);

        return $"{hijriDay:D2}/{hijriMonth:D2}/{hijriYear:D4}";
    }
}