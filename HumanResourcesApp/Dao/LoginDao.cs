using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp.Dao
{
    public class LoginDao
    {
        private SqliteConnection connection = new("Data Source=Sql\\sqlite\\human_resource_app.db");

        public string GetPassword(string username)
        {
            string password = "";

            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    SELECT login_password
                    FROM login
                    WHERE login_name = $username
                ";
                command.Parameters.AddWithValue("$username", username);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    password = reader.GetString(0);
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

            return password;
        }

        public int GetEmployeeID(string username)
        {
            int employeeID = 0;
            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    SELECT employee_id
                    FROM login
                    WHERE login_name = $username
                ";
                command.Parameters.AddWithValue("$username", username);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employeeID = reader.GetInt32(0);
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

            return employeeID;
        }
    }
}