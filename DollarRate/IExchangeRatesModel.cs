using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarRate
{
    /// <summary>
    /// Интерфейс для взаимодействия с моделью
    /// </summary>
    public interface IExchangeRatesModel
    {
        Tuple<DateTime, DateTime> GetPeriod();
        List<Rate> GetRates(Tuple<DateTime, DateTime> period);
    }
}
