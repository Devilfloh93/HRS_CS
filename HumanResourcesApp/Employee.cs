using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    internal class Employee : Person
    {
        public double Salary { get; private set; }
        public int MaxHolidays { get; private set; }

        public List<DateOnly> UsedHolidays
        {
            get;
        } = [];

        public List<DateOnly> SickDays
        {
            get;
        } = [];

        public List<DateOnly> PlannedHolidays
        {
            get;
        } = [];

        public List<DateOnly> Attendance
        {
            get;
        } = [];

        public List<DateOnly> WorkDays
        {
            get;
        } = [];

        public Employee(string firstname, string middleName, string lastname, string gender, DateOnly birthDate, string streetAdress, string postalCode, string city, string state, string country, double salary, int maxHolidays)
            : base(firstname, middleName, lastname, gender, birthDate, streetAdress, postalCode, city, state, country)
        {
            Salary = salary;
            MaxHolidays = maxHolidays;
        }
    }
}