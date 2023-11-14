using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageApp
{
    internal class Validator
    {

        public static bool ValidateDate(int day, int month, int year)
        {
            // Check if the year is valid (adjust the range as needed)
            if (year < 2012 || year > 2024)
            {
                Console.WriteLine("Invalid year.");
                return false;
            }

            // Check if the month is valid (1 to 12)
            if (month < 1 || month > 12)
            {
                Console.WriteLine("Invalid month.");
                return false;
            }

            // Check if the day is valid for the given month
            int[] daysInMonth = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // Adjust for a leap year (February has 29 days)
            if (IsLeapYear(year))
            {
                daysInMonth[2] = 29;
            }

            if (day < 1 || day > daysInMonth[month])
            {
                Console.WriteLine("Invalid day.");
                return false;
            }

            return true;
        }

        static bool IsLeapYear(int year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }
    }
}
