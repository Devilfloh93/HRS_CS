using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HumanResourcesApp.Dao
{
    public readonly struct EmployeeData
    {
        public string Firstname { get; init; }
        public string Middlename { get; init; }
        public string Lastname { get; init; }
        public string Gender { get; init; }
        public DateOnly BirthDate { get; init; }
        public string StreetAdress { get; init; }
        public string PostalCode { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public double Salary { get; init; }
        public int MaxHolidays { get; init; }

        public EmployeeData(string firstname, string middlename, string lastname, string gender, DateTime birthdate, string streetAdress, string postalCode,
            string city, string state, string country, string email, string phone, double salary, int maxHolidays)
        {
            Firstname = firstname;
            Middlename = middlename;
            Lastname = lastname;
            Gender = gender;
            BirthDate = new DateOnly(birthdate.Year, birthdate.Month, birthdate.Day);
            StreetAdress = streetAdress;
            PostalCode = postalCode;
            City = city;
            State = state;
            Country = country;
            Email = email;
            Phone = phone;
            Salary = salary;
            MaxHolidays = maxHolidays;
        }
    }

    public class EmployeeDao
    {
        private string dbPath = "Sql\\sqlite\\human_resource_app.db";

        public EmployeeData GetEmployeeData(int id)
        {
            EmployeeData employeeData = new();
            using var connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
            @"
                    SELECT *
                    FROM employee
                    WHERE employee_id = $id
            ";
            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                employeeData = new(
                   reader.GetString(0),
                   reader.GetString(1),
                   reader.GetString(2),
                   reader.GetString(3),
                   reader.GetDateTime(4),
                   reader.GetString(5),
                   reader.GetString(6),
                   reader.GetString(7),
                   reader.GetString(8),
                   reader.GetString(9),
                   reader.GetString(10),
                   reader.GetString(11),
                   reader.GetDouble(13),
                   reader.GetInt32(14));
            }

            // string firstname, string middlename, string lastname, string gender, DateOnly birthdate, string streetAdress, string postalCode,
            // string city, string state, string country, string email, string phone, double salary, int maxHolidays

            return employeeData;
        }
    }
}