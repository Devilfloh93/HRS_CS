using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
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

namespace HumanResourcesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user = new("Florian", "", "Pötzsch", "männlich", new DateOnly(1993, 08, 13), "Boxberger Str. 13", "01239", "Dresden", "Sachsen", "Germany", 20000.50, 24);

        public MainWindow()
        {
            InitializeComponent();

            user.SetLoggedInDate();

            DashboardGrid.ItemsSource = Task.LoadTaskData();

            List<ClockIn> clockIns = ClockIn.LoadClockInData();
            for (int i = 0; i < clockIns.Count; i++)
            {
                TimeDashboardCombo.Items.Add(clockIns[i].Categorie);
            }

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(UpdateBreakTimer);
            dispatcherTimer.Tick += new EventHandler(UpdateWorkTimer);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();

            DateOnly newDate = new(1993, 08, 13);

            Employee employee = new("Florian", "", "Pötzsch", "männlich", newDate, "Boxberger Str. 13", "01239", "Dresden", "Sachsen", "Germany", 20000.50, 24);

            employee.UsedHolidays.Add(new DateOnly(2004, 09, 02));
            employee.UsedHolidays.Add(new DateOnly(2004, 09, 03));
            employee.UsedHolidays.Add(new DateOnly(1984, 05, 15));

            employee.SickDays.Add(new DateOnly(1998, 11, 15));
            employee.SickDays.Add(new DateOnly(1998, 11, 16));
            employee.SickDays.Add(new DateOnly(1998, 11, 17));

            for (int i = 0; i < employee.SickDays.Count; i++)
            {
                DateOnly date = employee.SickDays[i];
                Debug.WriteLine("SickDay am: " + date.Day + "." + date.Month + "." + date.Year);
            }

            for (int i = 0; i < employee.UsedHolidays.Count; i++)
            {
                DateOnly date = employee.SickDays[i];
                Debug.WriteLine("UsedHoliday am: " + date.Day + "." + date.Month + "." + date.Year);
            }
        }

        private void DashboardGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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

            user.WorkTime.Start();
            user.BreakTime.Stop();

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
            user.WorkTime.Stop();
            user.BreakTime.Start();

            BreakBtn.Visibility = Visibility.Hidden;
            ClockInBtn.Visibility = Visibility.Visible;
        }

        private void UpdateWorkTimer(object sender, EventArgs e)
        {
            TimeSpan timeSpan = user.WorkTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            WorkTimer.Content = elapsedTime;

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void UpdateBreakTimer(object sender, EventArgs e)
        {
            TimeSpan timeSpan = user.BreakTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            BreakTimer.Content = elapsedTime;

            CommandManager.InvalidateRequerySuggested();
        }
    }
}