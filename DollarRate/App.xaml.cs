using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DollarRate
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
        }
        [STAThread]
        static void Main()
        {
            IExchangeRatesModel model = new ExchangeRatesModel();
            MainWindow view = new MainWindow();
            RatesPresenter presenter = new RatesPresenter(model, view);
            App app = new App();
            app.Run(view);
        }
    }
}
