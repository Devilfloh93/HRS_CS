using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    internal class Employee : PersonalData
    {
        private List<DateFormat> sickDays = [];

        private List<DateFormat> usedHolidays = [];

        public double Salary { get; private set; }
        public int MaxHolidays { get; private set; }

        public List<DateFormat> UsedHolidays
        {
            get => usedHolidays;
            set
            {
                usedHolidays = value;
            }
        }

        public List<DateFormat> SickDays
        {
            get => sickDays;
            set
            {
                sickDays = value;
            }
        }

        public Employee(string firstname, string lastname, DateFormat birthDate, string streetAdress, string postalCode, string city, string state, string country, double salary, int holidays)
            : base(firstname, lastname, birthDate, streetAdress, postalCode, city, state, country)
        {
            Salary = salary;
            MaxHolidays = holidays;
        }
    }
}