using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace DollarRate
{
    /// <summary>
    /// Интерфейс для взаимодействия с представлением
    /// </summary>
    public interface IRatesView
    {
        void ViewCurrentRate(List<Rate> rates);
        void SetAutoUpdate();
        void UnsetAutoUpdate();
        void SetSource(RatesCollection rateCollection);
        event EventHandler UpdateRate;
        event EventHandler<SelectionChangedEventArgs> AutoUpdateTimeChange;
        event EventHandler AutoUpdateChecked;
        event EventHandler AutoUpdateUnChecked;
        event EventHandler SetDataSource;
    }
}
