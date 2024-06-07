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

    internal class PersonalData
    {
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public DateFormat BirthDate { get; private set; }
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

                return thisDay.Year - BirthDate.Year;
            }
        }

        public PersonalData(string firstname, string lastname, DateFormat birthDate, string streetAdress, string postalCode, string city, string state, string country)
        {
            Firstname = firstname;
            Lastname = lastname;
            BirthDate = birthDate;
            StreetAdress = streetAdress;
            PostalCode = postalCode;
            City = city;
            State = state;
            Country = country;
        }
    }
}