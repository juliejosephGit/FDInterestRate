using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinModel.Model
{
    public class FDInterestRateResponse
    {
        [JsonProperty("success")]
        public bool success;
        [JsonProperty("result")]
        public result result;
    }
    public class FDInterestRate
    {
        [DisplayName("Year")]
        public string year
        {
            get
            {

                int index = end_of_month.IndexOf("-");
                string yr = (index > 0 ? end_of_month.Substring(0, index) : "");

                return yr;
            }
        }

        [DisplayName("Month")]
        public string month
        {
            get
            {
                int index = end_of_month.IndexOf("-");
                string mn = (index > 0 ? end_of_month.Substring(index + 1, end_of_month.Length - 1 - index) : "0");

                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(mn));
            }
        }

        [DisplayName("Fixed deposit interest for 3 months (Banks/Finance Company)")]
        public string fixed_deposits_3m {
            get
            {
                

                return banks_fixed_deposits_3m.ToString() + "/" + fc_fixed_deposits_3m.ToString();
            }
        }

        [DisplayName("Fixed deposit interest for 6 months (Banks/Finance Company)")]
        public string fixed_deposits_6m
        {
            get
            {


                return banks_fixed_deposits_6m.ToString() + "/" + fc_fixed_deposits_6m.ToString();
            }
        }

        [DisplayName("Fixed deposit interest for 12 months (Banks/Finance Company)")]
        public string fixed_deposits_12m
        {
            get
            {


                return banks_fixed_deposits_12m.ToString() + "/" + fc_fixed_deposits_12m.ToString();
            }
        }

        public bool fc_higher_fixed_deposits_3m {
            get
            {
                return fc_fixed_deposits_3m > banks_fixed_deposits_3m;

            }
        }

        public bool fc_higher_fixed_deposits_6m
        {
            get
            {
                return fc_fixed_deposits_6m > banks_fixed_deposits_6m;

            }
        }

        public bool fc_higher_fixed_deposits_12m
        {
            get
            {
                return fc_fixed_deposits_12m > banks_fixed_deposits_12m;

            }
        }

        [JsonProperty("end_of_month")]
        [DisplayName("End Of Month")]
        public string end_of_month { get; set; }
        
        [JsonProperty("banks_fixed_deposits_3m")]
        [DisplayName("Banks fixed deposit for 3 months")]
        public double banks_fixed_deposits_3m { get; set; }

        [JsonProperty("banks_fixed_deposits_6m")]
        [DisplayName("Banks fixed deposit for 6 months")]
        public double banks_fixed_deposits_6m { get; set; }

        [JsonProperty("banks_fixed_deposits_12m")]
        [DisplayName("Banks fixed deposit for 12 months")]
        public double banks_fixed_deposits_12m { get; set; }

        [JsonProperty("fc_fixed_deposits_3m")]
        [DisplayName("Finance Company fixed deposit for 3 months")]
        public double fc_fixed_deposits_3m { get; set; }

        [JsonProperty("fc_fixed_deposits_6m")]
        [DisplayName("Finance Company fixed deposit for 6 months")]
        public double fc_fixed_deposits_6m { get; set; }

        [JsonProperty("fc_fixed_deposits_12m")]
        [DisplayName("Finance Company fixed deposit for 12 months")]
        public double fc_fixed_deposits_12m { get; set; }

        
    }

    public class result
    {
        [JsonProperty("records")]
        public List<FDInterestRate> records { get; set; }
    }
}
