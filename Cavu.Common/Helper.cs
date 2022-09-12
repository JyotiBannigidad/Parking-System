using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cavu.Common
{
    public static class Helper
    {

        public static int GetNoOfDays(DateTime startDate, DateTime endDate)
        {
            int days = endDate.Subtract(startDate).Days;
            var days1 = Enumerable.Range(0, days)
                             .Select(day => startDate.AddDays(day))
                             .Count();

            return days1;

        }

        public static List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date.Date);
            return allDates;

        }
    }
}
