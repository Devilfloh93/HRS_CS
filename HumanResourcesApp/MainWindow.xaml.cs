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
using System.Windows.Input.Manipulations;
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
        public User? User { get; private set; }
        private readonly EmployeeDao employeeDao = new();

        public MainWindow(int employeeID)
        {
            InitializeComponent();
            UserDAO userDao = new();
            User = userDao.Create(employeeID);

            if (User == null)
            {
                this.Close();
                return;
            }
            else
            {
                this.Show();

                User.InitDashboard(this);

                InitDispatcher();
            }
        }

        private void InitDispatcher()
        {
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
            if (DashboardClockInCombo.SelectedIndex == -1)
                return;

            if (User.WorkTime.Elapsed.TotalSeconds == 0)
                employeeDao.InsertWorkTime(User.EmployeeID);

            if (User.BreakTime.Elapsed.TotalSeconds == 0)
                employeeDao.InsertBreakTime(User.EmployeeID);
            else
                employeeDao.UpdateBreakTime(User);

            User.WorkTime.Start();
            User.BreakTime.Stop();

            switch (Enum.ToObject(typeof(ClockInCombo), DashboardClockInCombo.SelectedIndex))
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
            var result = CreateMessageBox("Willst du wirklich in die Pause gehen?", "Bestätigung Pause", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                employeeDao.UpdateWorkTime(User);

                User.WorkTime.Stop();
                User.BreakTime.Start();

                BreakBtn.Visibility = Visibility.Hidden;
                ClockInBtn.Visibility = Visibility.Visible;
            }
        }

        private MessageBoxResult CreateMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            // TODO: IMPLEMENT NEW STYLE FOR MESSAGEBOX
            return MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
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