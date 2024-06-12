using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesApp
{
    internal enum ClockInCombo
    {
        None = -1,
        RemoteClockIn,
        WorkFromHome,
        WorkAtCompany
    }

    public class ClockIn
    {
        public string Categorie { get; private set; }

        public static List<ClockIn> LoadClockInData()
        {
            List<ClockIn> clockIns = [];
            clockIns.Add(new ClockIn("Von Unterwegs Einstempeln"));
            clockIns.Add(new ClockIn("Von Zuhause Arbeiten"));
            clockIns.Add(new ClockIn("Vor Ort Im Unternehmen"));

            return clockIns;
        }

        public ClockIn(string categorie)
        {
            Categorie = categorie;
        }
    }
}