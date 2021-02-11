using System.Collections.Generic;
using System.Windows.Data;

namespace DollarRate
{
    ///<summary>
    /// Класс для хранения данных о курсе доллара (дата, курс, изменение)
    /// </summary>
    public class Rate 
    {
        public string Date { get; set; }
        public double CurrentRate { get; set; }
        public double Change { get; set; }
    }
    ///<summary>
    /// Класс, содержащий колекцию для отображения модели
    /// </summary>
    public class RatesCollection 
    {
        public ListCollectionView RatesListCollection { get; set; }
        public List<Rate> RatesList { get; set; }
        public RatesCollection(List<Rate> rates)
        {
            if (rates != null)
            {
                // настройка коллекции отображения на модель
                RatesListCollection = new ListCollectionView(rates);
                RatesList = rates;
            }
        }
    }
}
