using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp.Dao
{
    public class LoginDao
    {
        private string dbPath = "Sql\\sqlite\\human_resource_app.db";

        public string GetPassword(string username)
        {
            string password = "";
            using var connection = new SqliteConnection("Data Source=" + dbPath);
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

            return password;
        }
    }
}