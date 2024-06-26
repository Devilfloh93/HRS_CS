﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HumanResourcesApp.Dao
{
    public class EmployeeDao
    {
        private readonly SqliteConnection connection = new("Data Source=Sql\\sqlite\\human_resource_app.db");

        public List<Employee> GetAll()
        {
            List<Employee> employeeData = [];

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
                    INNER JOIN personal_data p on e.employee_id = p.person_id
            ";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employeeData.Add(new(
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
                       reader.GetString(14)));
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

            return employeeData;
        }

        public List<WorkSchedule> GetWorkSchedule(int employeeID)
        {
            List<WorkSchedule> workSchedule = [];

            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    SELECT w.schedule_start,
                    w.schedule_end
                    FROM work_schedule w
                    WHERE employee_id = $employeeID
                ";
                command.Parameters.AddWithValue("$employeeID", employeeID);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    workSchedule.Add(new(
                       reader.GetDateTime(0),
                       reader.GetDateTime(1)));
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

            return workSchedule;
        }

        public List<Attendance> GetAttendance(int employeeID)
        {
            List<Attendance> attendance = [];

            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    SELECT w.created_at,
                    w.work_time_in_sec
                    FROM work_time w
                    WHERE employee_id = $employeeID
                ";
                command.Parameters.AddWithValue("$employeeID", employeeID);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    attendance.Add(new(
                       reader.GetDateTime(0),
                       reader.GetInt32(1)));
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

            return attendance;
        }

        public void InsertWorkTime(int employeeID)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    INSERT INTO work_time (employee_id, created_at) VALUES (
                    $employeeID,
                    datetime('now')
                )
                ";
                command.Parameters.AddWithValue("$employeeID", employeeID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateWorkTime(User user)
        {
            if (user.WorkTime.Elapsed.TotalSeconds > 0)
            {
                try
                {
                    connection.Open();

                    var command = connection.CreateCommand();

                    command.CommandText =
                    @"
                        UPDATE work_time SET
                        work_time_in_sec = $workTimeInSec
                        WHERE employee_id = $employeeID AND strftime('%Y-%m-%d', created_at) = strftime('%Y-%m-%d', 'now')
                    ";
                    command.Parameters.AddWithValue("$workTimeInSec", (int)user.WorkTime.Elapsed.TotalSeconds);
                    command.Parameters.AddWithValue("$employeeID", user.EmployeeID);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR: " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void InsertBreakTime(int employeeID)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText =
                @"
                    INSERT INTO break_time (employee_id, created_at) VALUES (
                    $employeeID,
                    datetime('now')
                    )
                ";
                command.Parameters.AddWithValue("$employeeID", employeeID);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateBreakTime(User user)
        {
            if (user.BreakTime.Elapsed.TotalSeconds > 0)
            {
                try
                {
                    connection.Open();

                    var command = connection.CreateCommand();

                    command.CommandText =
                    @"
                        UPDATE break_time SET
                        break_time_in_sec = $breakTimeInSec
                        WHERE employee_id = $employeeID AND strftime('%Y-%m-%d', created_at) = strftime('%Y-%m-%d', 'now')
                    ";
                    command.Parameters.AddWithValue("$breakTimeInSec", (int)user.BreakTime.Elapsed.TotalSeconds);
                    command.Parameters.AddWithValue("$employeeID", user.EmployeeID);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR: " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}