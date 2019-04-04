using FinModel.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinModel.ViewModel
{
    public class MainVM : ViewModel
    {
        public List<Month> MonthList { get; set; }
        public List<int> YearList { get; set; }

        public List<FDInterestRate> FDInterestRateList
        {
            get { return this._FDInterestRateList; }
            set
            {
                this._FDInterestRateList = value;
                this.NotifyPropertyChanged("FDInterestRateList");
            }
        }

        private Month selectedStartMonth;
        private int selectedStartYear;
        private Month selectedEndMonth;
        private int selectedEndYear;
        private int highlight;

        private List<FDInterestRate> _FDInterestRateList;

        public Month SelectedStartMonth
        {
            get { return this.selectedStartMonth; }
            set
            {
                this.selectedStartMonth = value;
                this.NotifyPropertyChanged("SelectedStartMonth");
            }
        }

        public int SelectedStartYear
        {
            get { return this.selectedStartYear; }
            set
            {
                this.selectedStartYear = value;
                this.NotifyPropertyChanged("SelectedStartYear");
            }
        }

        public Month SelectedEndMonth
        {
            get { return this.selectedEndMonth; }
            set
            {
                this.selectedEndMonth = value;
                this.NotifyPropertyChanged("SelectedEndMonth");
            }
        }

        public int SelectedEndYear
        {
            get { return this.selectedEndYear; }
            set
            {
                this.selectedEndYear = value;
                this.NotifyPropertyChanged("SelectedEndYear");
            }
        }

        public int Highlight
        {
            get { return this.highlight; }
            set
            {
                this.highlight = value;
                this.NotifyPropertyChanged("Highlight");
            }
        }
        public MainVM()
        {
            try
            {
                highlight = 0;
                MonthList = Singleton.SingleInstance.Months;
                YearList = Singleton.SingleInstance.Years;

                SelectedStartMonth = new Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month), DateTime.Now.Month);
                SelectedStartYear = DateTime.Now.Year;

                SelectedEndMonth = new Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month), DateTime.Now.Month);
                SelectedEndYear = DateTime.Now.Year;

                GetFDInterestRateList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public void GetFDInterestRateList()
        {
            try
            {
                string startDate = SelectedStartYear + "-" + (SelectedStartMonth.monthNumber).ToString("D2");
                string endDate = SelectedEndYear + "-" + SelectedEndMonth.monthNumber.ToString("D2");
                var FDInterest = Singleton.SingleInstance.GetFDInterest(startDate, endDate, highlight);

                FDInterestRateList = FDInterest.result.records;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
