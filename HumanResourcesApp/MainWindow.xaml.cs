using HumanResourcesApp.Dao;
using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HumanResourcesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User User { get; private set; }
        private readonly EmployeeDao employeeDao = new();

        public MainWindow(int employeeID)
        {
            InitializeComponent();

            Employee? userEmployeeData = employeeDao.GetSingle(employeeID);

            if (userEmployeeData == null) { this.Close(); }

            User = new(employeeID, userEmployeeData.Salary, userEmployeeData.MaxHolidays, userEmployeeData.FirstName, userEmployeeData.MiddleName, userEmployeeData.LastName,
                userEmployeeData.Gender, userEmployeeData.Birthdate, userEmployeeData.StreetAdress, userEmployeeData.PostalCode, userEmployeeData.City, userEmployeeData.State,
                userEmployeeData.Country, userEmployeeData.Email, userEmployeeData.Phone, userEmployeeData.JobTitle);

            User.SetLoggedInDate();

            if (User.MiddleName.Length > 0)
                EmployeeName.Content = User.FirstName + " " + User.MiddleName + " " + User.LastName;
            else
                EmployeeName.Content = User.FirstName + " " + User.LastName;

            JobTitle.Content = User.JobTitle;

            List<Employee> employeeData = employeeDao.GetAll();
            for (int i = 0; i < employeeData.Count; i++)
            {
                Debug.WriteLine(employeeData[i].Country);
            }

            DashboardDataGrid.ItemsSource = Task.LoadTaskData();

            List<ClockIn> clockIns = ClockIn.LoadClockInData();
            for (int i = 0; i < clockIns.Count; i++)
            {
                TimeDashboardCombo.Items.Add(clockIns[i].Categorie);
            }

            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += (sender, e) => { UpdateBreakTimer(); };
            dispatcherTimer.Tick += (sender, e) => { UpdateWorkTimer(); };
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();

            DispatcherTimer dispatcherTimer1 = new();
            dispatcherTimer1.Tick += (sender, e) => { employeeDao.UpdateBreakTime(User); };
            dispatcherTimer1.Tick += (sender, e) => { employeeDao.UpdateWorkTime(User); };
            dispatcherTimer1.Interval = TimeSpan.FromMinutes(1);
            dispatcherTimer1.Start();
        }

        private void DashboardDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            switch (headername)
            {
                case "ProjectName":
                    e.Column.Header = "Projekt";
                    break;

                case "TaskName":
                    e.Column.Header = "Aufgabe";
                    break;

                case "WorkLogs":
                    e.Column.Header = "Bearbeitungsdauer";
                    break;

                case "DueDate":
                    e.Column.Header = "Fälligkeitsdatum";
                    break;
            }
        }

        private void Button_Click_ClockIn(object sender, RoutedEventArgs e)
        {
            if (TimeDashboardCombo.SelectedIndex == -1)
                return;

            if (User.WorkTime.Elapsed.Seconds == 0)
            {
                employeeDao.InsertWorkTime(User.EmployeeID);
            }

            User.WorkTime.Start();
            User.BreakTime.Stop();

            switch (Enum.ToObject(typeof(ClockInCombo), TimeDashboardCombo.SelectedIndex))
            {
                case ClockInCombo.RemoteClockIn:
                    Debug.WriteLine("Remote Clock in");
                    break;

                case ClockInCombo.WorkFromHome:
                    Debug.WriteLine("Work from home");
                    break;

                case ClockInCombo.WorkAtCompany:
                    Debug.WriteLine("Work at Company");
                    break;
            }

            ClockInBtn.Visibility = Visibility.Hidden;
            BreakBtn.Visibility = Visibility.Visible;
        }

        private void Button_Click_Break(object sender, RoutedEventArgs e)
        {
            if (User.BreakTime.Elapsed.Seconds == 0)
            {
                employeeDao.InsertBreakTime(User.EmployeeID);
            }

            User.WorkTime.Stop();
            User.BreakTime.Start();

            BreakBtn.Visibility = Visibility.Hidden;
            ClockInBtn.Visibility = Visibility.Visible;
        }

        private void UpdateWorkTimer()
        {
            TimeSpan timeSpan = User.WorkTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            WorkTimer.Content = elapsedTime;

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateBreakTimer()
        {
            TimeSpan timeSpan = User.BreakTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            BreakTimer.Content = elapsedTime;

            CommandManager.InvalidateRequerySuggested();
        }

        private void Button_Click_ShowProfile(object sender, RoutedEventArgs e)
        {
            DashboardGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_ShowDashboard(object sender, RoutedEventArgs e)
        {
            DashboardGrid.Visibility = Visibility.Visible;
        }
    }
}