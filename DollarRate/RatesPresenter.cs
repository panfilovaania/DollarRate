using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace DollarRate
{
    /// <summary>
    /// Класс, реализующий связь модели и представления
    /// </summary>
    public class RatesPresenter
    {
        private IExchangeRatesModel model;
        private IRatesView view;
        private Timer timer;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">оБъект для доступа к методам модели</param>
        /// <param name="view">объект для доступа к методам предсталения</param>
        public RatesPresenter(IExchangeRatesModel model, IRatesView view)
        {
            this.model = model;
            this.view = view;
            this.view.UpdateRate += new EventHandler(View_UpdateRate);
            this.view.AutoUpdateTimeChange += View_AutoUpdateTimeChange;
            this.view.AutoUpdateChecked += new EventHandler(View_SetAutoUpdate);
            this.view.AutoUpdateUnChecked += new EventHandler(View_UnsetAutoUpdate);
            this.view.SetDataSource += new EventHandler(View_SetDataSource);
        }
        /// <summary>
        /// Метод, реализующий обработчик события щелчка по кнопке Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_UpdateRate(object sender, EventArgs e)
        {                
            View_SetDataSource(this, e);
        }
        /// <summary>
        /// Метод, реализующий обработчик события изменения времени обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_AutoUpdateTimeChange(object sender, SelectionChangedEventArgs e) 
        {
            timer = new Timer();
            timer.Interval = Convert.ToDouble(e.AddedItems[0]) * 6000;
            timer.Elapsed += View_SetDataSource;
            timer.AutoReset = false;
            timer.Start();
        }
        /// <summary>
        /// Метод, реализуюший обработчик события установки Автоматического обновления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_SetAutoUpdate(object sender, EventArgs e)
        {
            view.SetAutoUpdate();
        }
       /// <summary>
       /// Метод, реализующий обработчик события отключения Автомаического обновления
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void View_UnsetAutoUpdate(object sender, EventArgs e)
        {
            view.UnsetAutoUpdate();
            if (timer != null)
            timer.Stop();
        }
       /// <summary>
       /// Метод, реализующий событие загрузки формы
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void View_SetDataSource(object sender, EventArgs e) 
        {
            view.ViewCurrentRate(model.GetRates(model.GetPeriod()));
            view.SetSource(new RatesCollection(model.GetRates(model.GetPeriod())));
        }
    }
}
