﻿using FinModel.Model;
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
        private static readonly string address = @"https://eservices.mas.gov.sg/api/action/datastore/search.json?resource_id=5f2b18a8-0883-4769-a635-879c63d3caac&limit={0}&between[end_of_month]={1},{2}";
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


        public int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }
        public FDInterestRateResponse GetFDInterest(string startDate, string endDate, int highlight=0)
        {
            try
            {
                var result = GetFDInterestFromAPI(startDate, endDate);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var FDInterestRateResponse = JsonConvert.DeserializeObject<FDInterestRateResponse>(result, settings);
                foreach(var record in FDInterestRateResponse.result.records)
                {
                    record.highlight = highlight;
                }
                return FDInterestRateResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public  string GetFDInterestFromAPI(string startDate, string endDate)
        {
            try
            {
                DateTime stDate = DateTime.Parse(startDate+"-01");
                DateTime enDate = DateTime.Parse(endDate + "-28");
                int mns = GetMonthDifference(stDate, enDate);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new HttpClient();
                var requestUri = string.Format(address, mns+1,startDate, endDate);
                HttpResponseMessage response = client.GetAsync(requestUri).Result;
                response.EnsureSuccessStatusCode();
                var result = response.Content.ReadAsStringAsync().Result;
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
