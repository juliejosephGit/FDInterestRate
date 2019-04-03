using FinModel.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinModel
{
    sealed class Singleton
    {
        static int instanceCounter = 0;
        private static Singleton singleInstance = null;
        private static readonly object lockObject = new object();
        private static readonly int startYear = 1983;
        //        private static readonly string address = " https://eservices.mas.gov.sg/api/action/datastore/search.json?resource_id=5f2b18a8-0883-4769-a635-879c63d3caac&between[end_of_month]=2008-01,2010-12";
        private static readonly string address = @"https://eservices.mas.gov.sg/api/action/datastore/search.json?resource_id=5f2b18a8-0883-4769-a635-879c63d3caac&between[end_of_month]={0},{1}";
        private Singleton()
        {
            instanceCounter++;
            
        }

        public static Singleton SingleInstance
        {
            get
            {
                if (singleInstance == null)
                {
                    lock (lockObject)
                    {
                        if (singleInstance == null)
                        {
                            singleInstance = new Singleton();
                        }

                    }
                }
                return singleInstance;
            }
        }
        public List<Month> Months
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Select((monthName, index) => new Month(monthName, (index + 1))).ToList();
                       
            }
        }

        public List<int> Years
        {
            get
            {
                //return Enumerable.Range(startYear, DateTime.Now.Year).ToList();

                return Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1)
                        .ToList();

            }
        }



        //public FDInterestRateResponse GetFDInterest()
        //{
        //    try
        //    {
        //        return  GetFDInterestFromAPI();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public async Task<FDInterestRateResponse> GetFDInterestFromAPI()
        //{
        //    try
        //    {
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        var client = new HttpClient();
        //        HttpResponseMessage response = await client.GetAsync(address);
        //        response.EnsureSuccessStatusCode();
        //        var result = await response.Content.ReadAsStringAsync();
        //        var FDInterestRateResponse = JsonConvert.DeserializeObject<FDInterestRateResponse>(result);
        //        return FDInterestRateResponse;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public  FDInterestRateResponse GetFDInterestFromAPI(string startDate, string endDate)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new HttpClient();
                var requestUri = string.Format(address, startDate, endDate);
                HttpResponseMessage response = client.GetAsync(requestUri).Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var FDInterestRateResponse = JsonConvert.DeserializeObject<FDInterestRateResponse>(result, settings);
                return FDInterestRateResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
