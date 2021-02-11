using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Windows;
using System.Xml.Linq;

namespace DollarRate
{
    /// <summary>
    /// Класс, содержащий методы получения и обработки данных
    /// </summary>
    public class ExchangeRatesModel : IExchangeRatesModel
    {
        public List<Rate> ratesList;
        public ExchangeRatesModel()
        {
            this.ratesList = GetRates(GetPeriod());
        }
        /// <summary>
        /// Метод, возвращающий значения курса за указанный период из XML и величину изменения
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public List<Rate> GetRates(Tuple<DateTime, DateTime> period)
        {
            try
            {
                UriBuilder builder = new UriBuilder("http://www.cbr.ru/scripts/XML_dynamic.asp")
                {
                    Port = -1
                };

                var query = HttpUtility.ParseQueryString(builder.Query);

                query["date_req1"] = period.Item2.ToShortDateString();
                query["date_req2"] = period.Item1.ToShortDateString();
                query["VAL_NM_RQ"] = "R01235";

                builder.Query = query.ToString();

                string url = builder.ToString();

                HttpClient client = new HttpClient();

                string xml = client.GetStringAsync(url).Result;

                XDocument xdoc = XDocument.Parse(xml);

                ratesList = xdoc.Element("ValCurs").Elements("Record").Select(x => new Rate { Date = x.Attribute("Date").Value.ToString(), CurrentRate = Convert.ToDouble(x.Element("Value").Value.ToString()), Change = 0 }).ToList();

                for (int i = 1; i < ratesList.Count; i++)
                {
                    ratesList[i].Change = ratesList[i].CurrentRate - ratesList[i - 1].CurrentRate;
                }
                return ratesList;
            }   
            catch (AggregateException)
            {
                MessageBox.Show("Не удалось получить данные с удалённого сервиса.", "Ошибка доступа к удалённому сервису", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
}

        /// <summary>
        /// Метод, возращающий кортеж, содержащий текущую дату и дату начала периода 
        /// </summary>
        /// <returns></returns>
        public Tuple<DateTime, DateTime> GetPeriod()
        {
            DateTime currentDate = DateTime.Today;
            DateTime weekBefore = DateTime.Today.AddDays(-8);
            return Tuple.Create(currentDate, weekBefore);
        }

    }
}
