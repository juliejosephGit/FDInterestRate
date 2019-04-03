using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinModel.Model
{
    public class Month
    {
        public string monthName { get; set; }
        public int monthNumber { get; set; }

        public Month(string MonthName,
                         int MonthNumber)
        {
            monthName = MonthName;
            monthNumber = MonthNumber;
        }
    }
}
