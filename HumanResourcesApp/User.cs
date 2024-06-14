using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    public class User : Employee
    {
        public Stopwatch WorkTime { get; private set; }
        public Stopwatch BreakTime { get; private set; }
        public DateTime LoggedIn { get; private set; }
        public int EmployeeID { get; private set; }

        public void SetLoggedInDate()
        {
            LoggedIn = DateTime.Now;
        }

        public User(int employeeID, double salary, int maxHolidays, string firstname, string middleName, string lastname, string gender, DateTime birthDate, string streetAdress, string postalCode, string city, string state, string country, string email, string phone, string jobTitle) : base(salary, maxHolidays, firstname, middleName, lastname, gender, birthDate, streetAdress, postalCode, city, state, country, email, phone, jobTitle)
        {
            WorkTime = new Stopwatch();
            BreakTime = new Stopwatch();
            EmployeeID = employeeID;
        }
    }
}