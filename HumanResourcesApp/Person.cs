using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    public readonly struct DateFormat
    {
        public DateFormat(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public int Day { get; init; }
        public int Month { get; init; }
        public int Year { get; init; }
    }

    internal class Person
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Gender { get; private set; }
        public DateFormat Birthdate { get; private set; }
        public string PostalCode { get; private set; }
        public string StreetAdress { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        public int Age
        {
            get
            {
                DateTime thisDay = DateTime.Today;

                return thisDay.Year - Birthdate.Year;
            }
        }

        public Person(string firstName, string middleName, string lastName, string gender, DateFormat birthdate, string streetAdress, string postalCode, string city, string state, string country)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            Birthdate = birthdate;
            StreetAdress = streetAdress;
            PostalCode = postalCode;
            City = city;
            State = state;
            Country = country;
        }
    }
}