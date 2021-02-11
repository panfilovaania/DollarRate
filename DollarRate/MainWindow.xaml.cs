using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DollarRate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRatesView
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        public event EventHandler UpdateRate;
        protected virtual void OnUpdateRate(EventArgs e)
        {
            if (UpdateRate != null)
            {
                UpdateRate?.Invoke(this, e);
            }
        }
        public event EventHandler<SelectionChangedEventArgs> AutoUpdateTimeChange;
        protected virtual void OnAutoUpdateTimeChange(SelectionChangedEventArgs e)
        {
            if (AutoUpdateTimeChange != null)
            {
                AutoUpdateTimeChange?.Invoke(this, e);
            }
        }
        public event EventHandler AutoUpdateChecked;
        protected virtual void OnAutoUpdateChecked(EventArgs e)
        {
            if (AutoUpdateChecked != null)
            {
                AutoUpdateChecked?.Invoke(this, e);
            }
        }
        public event EventHandler AutoUpdateUnChecked;
        protected virtual void OnAutoUpdateUnChecked(EventArgs e)
        {
            if (AutoUpdateUnChecked != null)
            {
                AutoUpdateUnChecked?.Invoke(this, e);
            }
        }
        public event EventHandler SetDataSource;
        protected virtual void OnSetDataSource(EventArgs e)
        {
            if (SetDataSource != null)
            {
                SetDataSource?.Invoke(this, e);
            }
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            OnUpdateRate(EventArgs.Empty);
        }
        private void Cmb_UpdateTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnAutoUpdateTimeChange(e);
        }
        private void Cb_AutoUpdate_Checked(object sender, RoutedEventArgs e)
        {
            OnAutoUpdateChecked(EventArgs.Empty);
        }
        private void Cb_AutoUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            OnAutoUpdateUnChecked(EventArgs.Empty);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            OnSetDataSource(EventArgs.Empty);
            int tmp = 1;
            while (tmp < 60)
            {
                Cmb_UpdateTime.Items.Add(tmp++);
            }
        }

        public void ViewCurrentRate(List<Rate> rates)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (rates != null) 
                {
                    Tbl_Date.Text = rates[rates.Count - 1].Date + ":";
                    Tbl_Rate.Text = rates[rates.Count - 1].CurrentRate.ToString();
                }
            }));
        }

        public void SetAutoUpdate()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                Cmb_UpdateTime.IsEnabled = true;
            }));
        }
        public void UnsetAutoUpdate()
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                Cmb_UpdateTime.IsEnabled = false;
            }));
        }
        public void SetSource(RatesCollection rateCollection) 
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (rateCollection.RatesList != null && rateCollection.RatesListCollection != null) 
                { 
                    mcChart.DataContext = rateCollection.RatesList.Select(x => new KeyValuePair<string, double>(x.Date, x.CurrentRate)).ToList();
                    DataContext = rateCollection;
                    MessageBox.Show("Данные обновлены");
                }
            }));
        }
    }
}
