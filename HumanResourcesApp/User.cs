using HumanResourcesApp.Dao;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    public class User : Employee
    {
        private readonly EmployeeDao employeeDao = new();
        public Stopwatch WorkTime { get; private set; }
        public Stopwatch BreakTime { get; private set; }
        public int EmployeeID { get; private set; }

        public void UpdateAttendance(MainWindow mainWindow)
        {
            int attPresent = 0;
            int attAbsent = 0;
            int attDelayed = 0;

            for (int i = 0; i < WorkSchedules.Count; i++)
            {
                bool wasAbsent = true;
                DateTime start = WorkSchedules[i].Start;

                for (int j = 0; j < Attendance.Count; j++)
                {
                    DateTime started = Attendance[j].Start;
                    if (start.Day == started.Day && start.Month == started.Month && start.Year == started.Year)
                    {
                        if (start.Hour == started.Hour || (start.Hour - 1) == started.Hour)
                            attPresent++;
                        else
                            attDelayed++;

                        wasAbsent = false;
                    }
                }

                if (wasAbsent)
                    attAbsent++;
            }

            mainWindow.DashboardAttPresent.Content = attPresent;
            mainWindow.DashboardAttAbsent.Content = attAbsent;
            mainWindow.DashboardAttDelayed.Content = attDelayed;
        }

        public void InitDashboard(MainWindow mainWindow)
        {
            if (MiddleName.Length > 0)
                mainWindow.DashboardEmployeeName.Content = FirstName + " " + MiddleName + " " + LastName;
            else
                mainWindow.DashboardEmployeeName.Content = FirstName + " " + LastName;

            mainWindow.DashboardJobTitle.Content = JobTitle;

            mainWindow.DashboardHolidayMax.Content = MaxHolidays;
            mainWindow.DashboardHolidayUsed.Content = UsedHolidays.Count;
            mainWindow.DashboardHolidayPlanned.Content = PlannedHolidays.Count;
            mainWindow.DashboardHolidayAvailable.Content = MaxHolidays - (UsedHolidays.Count - PlannedHolidays.Count);

            UpdateAttendance(mainWindow);
            UpdateTasks(mainWindow);
            UpdateClockIn(mainWindow);
        }

        public void UpdateTasks(MainWindow mainWindow)
        {
            List<Employee> employeeData = employeeDao.GetAll();
            mainWindow.DashboardDataGrid.ItemsSource = Task.LoadTaskData();
        }

        public void UpdateClockIn(MainWindow mainWindow)
        {
            List<ClockIn> clockIns = ClockIn.LoadClockInData();
            for (int i = 0; i < clockIns.Count; i++)
            {
                mainWindow.DashboardClockInCombo.Items.Add(clockIns[i].Categorie);
            }
        }

        public User(int employeeID, double salary, int maxHolidays, string firstname, string middleName, string lastname, string gender, DateTime birthDate, string streetAdress, string postalCode, string city, string state, string country, string email, string phone, string jobTitle) : base(salary, maxHolidays, firstname, middleName, lastname, gender, birthDate, streetAdress, postalCode, city, state, country, email, phone, jobTitle)
        {
            WorkTime = new Stopwatch();
            BreakTime = new Stopwatch();
            EmployeeID = employeeID;
        }
    }
}