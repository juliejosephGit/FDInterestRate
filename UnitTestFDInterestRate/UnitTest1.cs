using System;
using System.Globalization;
using FinModel.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestFDInterestRate
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Valid_GetFDInterestRateListLastMonth()
        {
            DateTime lastmonth = DateTime.Now.AddMonths(-1);
            MainVM mVM = new MainVM();
            mVM.SelectedStartMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(lastmonth.Month), lastmonth.Month);
            mVM.SelectedEndMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(lastmonth.Month), lastmonth.Month);
            mVM.SelectedStartYear = lastmonth.Year;
            mVM.SelectedEndYear = lastmonth.Year;
            mVM.GetFDInterestRateList();
            Assert.AreEqual(1, mVM.FDInterestRateList.Count);
        }
        [TestMethod]
        public void Valid_GetFDInterestRateListYear()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            MainVM mVM = new MainVM();
            mVM.SelectedStartMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(lastYear.Month), lastYear.Month);
            mVM.SelectedEndMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month), DateTime.Now.Month);
            mVM.SelectedStartYear = lastYear.Year;
            mVM.SelectedEndYear = DateTime.Now.Year;
            mVM.GetFDInterestRateList();
            Assert.AreEqual(true, mVM.FDInterestRateList.Count>0);
        }
    }
}
