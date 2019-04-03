using FinModel.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinServiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private MainVM mainVM;
    
        public MainWindow()
        {
            
            InitializeComponent();
            
            try
            {
                if (App.Args != null)
                {
                    String[] args = App.Args;

                    if (args.Count() == 2)
                    {
                        string startDate = args[0];
                        int sindex = startDate.IndexOf("-");
                        string yr1 = (sindex > 0 ? startDate.Substring(0, sindex) : "0");
                        string mn1 = (sindex > 0 ? startDate.Substring(sindex + 1, startDate.Length - 1 - sindex) : "0");

                        string endDate = args[1];
                        int eindex = endDate.IndexOf("-");
                        string yr2 = (eindex > 0 ? endDate.Substring(0, eindex) : "0");
                        string mn2 = (eindex > 0 ? endDate.Substring(eindex + 1, endDate.Length - 1 - eindex) : "0");


                        mainVM = new MainVM();
                        mainVM.SelectedStartMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(mn1)), Convert.ToInt32(mn1));
                        mainVM.SelectedEndMonth = new FinModel.Model.Month(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(mn2)), Convert.ToInt32(mn2));
                        mainVM.SelectedStartYear = Convert.ToInt32(yr1);
                        mainVM.SelectedEndYear = Convert.ToInt32(yr2);
                        mainVM.GetFDInterestRateList();
                    }
                }
                if(mainVM==null)
                    mainVM = new MainVM();
                DataContext = mainVM;
                

            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }


            

        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }

            if (e.PropertyName == "year" || e.PropertyName == "month" || e.PropertyName == "fixed_deposits_3m" || e.PropertyName == "fixed_deposits_6m" || e.PropertyName == "fixed_deposits_12m")
                e.Cancel = false;
            else
                e.Cancel = true;
            if(e.PropertyName== "fixed_deposits_3m")
                e.Column.CellStyle = (Style)Resources["3mGridCellStyle"];
            else if (e.PropertyName == "fixed_deposits_6m")
                e.Column.CellStyle = (Style)Resources["6mGridCellStyle"];
            else if (e.PropertyName == "fixed_deposits_12m")
                e.Column.CellStyle = (Style)Resources["12mGridCellStyle"];
            else 
                e.Column.CellStyle = (Style)Resources["GridCellStyle"];


        }

        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mainVM.GetFDInterestRateList();
        }
    }
}
