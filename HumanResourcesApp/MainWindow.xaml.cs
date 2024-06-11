using System.Diagnostics;
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
        public MainWindow()
        {
            InitializeComponent();

            DashboardGrid.ItemsSource = Task.LoadTaskData();

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
                    e.Column.Header = "Projekt  ";
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
    }
}