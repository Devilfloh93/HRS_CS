using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    public struct WorkSchedule
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public WorkSchedule(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }

    public struct Attendance
    {
        public DateTime Start { get; private set; }

        public int DurationInSec { get; set; }

        public Attendance(DateTime start, int durationInSec)
        {
            Start = start;
            DurationInSec = durationInSec;
        }
    }

    public class Employee : Person
    {
        public double Salary { get; private set; }
        public int MaxHolidays { get; private set; }
        public string JobTitle { get; private set; }

        public List<DateOnly> UsedHolidays
        {
            get;
        } = [];

        public List<DateOnly> PlannedHolidays
        {
            get;
        } = [];

        public List<DateOnly> SickDays
        {
            get;
        } = [];

        public List<WorkSchedule> WorkSchedules
        {
            get;
            set;
        } = [];

        public List<Attendance> Attendance
        {
            get;
            set;
        } = [];

        public Employee(double salary, int maxHolidays, string firstname, string middleName, string lastname, string gender,
                        DateTime birthDate, string streetAdress, string postalCode, string city, string state, string country, string email, string phone, string jobTitle)
            : base(firstname, middleName, lastname, gender, birthDate, streetAdress, postalCode, city, state, country, email, phone)
        {
            Salary = salary;
            MaxHolidays = maxHolidays;
            JobTitle = jobTitle;
        }
    }
}