using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace HumanResourcesApp.Dao
{
    public class UserDAO
    {
        private readonly SqliteConnection connection = new("Data Source=Sql\\sqlite\\human_resource_app.db");
        private readonly EmployeeDao employeeDao = new();

        public User? Create(int employeeID)
        {
            User? user = null;

            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    SELECT e.salary,
                    e.max_holidays,
	                p.firstname,
	                p.middlename,
	                p.lastname,
	                p.gender,
	                p.birth_date,
	                p.street_adress,
	                p.postal_code,
	                p.city,
	                p.state,
	                p.country,
	                p.email,
	                p.phone,
                    e.job_title
                    FROM employee e
                    INNER JOIN personal_data p ON e.person_id = p.person_id
                    WHERE employee_id = $employeeID
                ";
                command.Parameters.AddWithValue("$employeeID", employeeID);

                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user = new(
                    employeeID,
                    reader.GetDouble(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetDateTime(6),
                    reader.GetString(7),
                    reader.GetString(8),
                    reader.GetString(9),
                    reader.GetString(10),
                    reader.GetString(11),
                    reader.GetString(12),
                    reader.GetString(13),
                    reader.GetString(14));
                }

                if (user != null)
                {
                    user.WorkSchedules = employeeDao.GetWorkSchedule(employeeID);
                    user.Attendance = employeeDao.GetAttendance(employeeID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex);
            }
            finally
            {
                connection.Close();
            }

            return user;
        }
    }
}