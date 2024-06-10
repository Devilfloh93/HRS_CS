using System.Diagnostics;
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

            DateFormat newDate = new(13, 08, 1993);

            Employee employee = new("Florian", "", "Pötzsch", "männlich", newDate, "Boxberger Str. 13", "01239", "Dresden", "Sachsen", "Germany", 20000.50, 24);

            employee.UsedHolidays.Add(new DateFormat(02, 09, 2004));
            employee.UsedHolidays.Add(new DateFormat(03, 09, 2004));
            employee.UsedHolidays.Add(new DateFormat(15, 05, 1984));

            employee.SickDays.Add(new DateFormat(15, 11, 1998));
            employee.SickDays.Add(new DateFormat(16, 11, 1998));
            employee.SickDays.Add(new DateFormat(17, 11, 1998));

            for (int i = 0; i < employee.SickDays.Count; i++)
            {
                DateFormat date = employee.SickDays[i];
                Debug.WriteLine("SickDay am: " + date.Day + "." + date.Month + "." + date.Year);
            }

            for (int i = 0; i < employee.UsedHolidays.Count; i++)
            {
                DateFormat date = employee.SickDays[i];
                Debug.WriteLine("UsedHoliday am: " + date.Day + "." + date.Month + "." + date.Year);
            }
        }
    }
}