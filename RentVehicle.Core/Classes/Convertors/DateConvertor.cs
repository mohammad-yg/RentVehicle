using System;
using System.Globalization;

namespace RentVehicle.Core.Classes.Convertors
{
    public static class DateConvertor
    {
        public static string PersianDate(this PersianCalendar persian, DateTime dateTime)
        {
            var result = $"{persian.GetHour(dateTime).ToString().PadLeft(2, '0')}:{persian.GetMinute(dateTime).ToString().PadLeft(2, '0')}," +
                         $"{persian.GetYear(dateTime)}/{persian.GetMonth(dateTime).ToString().PadLeft(2, '0')}/{persian.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0')}";

            return result;
        }
    }
}