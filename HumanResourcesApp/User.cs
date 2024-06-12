using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    internal class User : Employee
    {
        public Stopwatch WorkTime { get; private set; }
        public Stopwatch BreakTime { get; private set; }
        public DateTime LoggedIn { get; private set; }

        public void SetLoggedInDate()
        {
            LoggedIn = DateTime.Now;
        }

        public User(string firstname, string middleName, string lastname, string gender, DateOnly birthDate, string streetAdress, string postalCode, string city, string state, string country, double salary, int maxHolidays) : base(firstname, middleName, lastname, gender, birthDate, streetAdress, postalCode, city, state, country, salary, maxHolidays)
        {
            WorkTime = new Stopwatch();
            BreakTime = new Stopwatch();
        }
    }
}