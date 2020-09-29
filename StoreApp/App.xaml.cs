using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.UI;
using StoreApp.UI.Application;
using StoreApp.UI.DataAccess;
using StoreApp.UI.Domain;
using System;
using System.Windows;

namespace StoreApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            App.ServiceProvider = serviceCollection.BuildServiceProvider();

            base.OnStartup(e);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient(typeof(IAppConfiguration), typeof(AppConfiguration));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Sales>();
            services.AddSingleton<Setup>();
            services.AddSingleton<Summary>();
            services.AddTransient<IPromotion, NonePromotion>();
            services.AddTransient<IPromotion, PercentagePromotion>();
            services.AddTransient<IPromotion, BogoPromotion>();
            services.AddTransient<IPromotionCalculator, NonePromotionCalculator>();
            services.AddTransient<IPromotionCalculator, PercentagePromotionCalculator>();
            services.AddTransient<IPromotionCalculator, BogoPromotionCalculator>();
            services.AddTransient<IPromoCalculatorFactory, PromoCalculatorFactory>();
            services.AddTransient<IProductStore, ProductStore>();
            services.AddTransient<IInvoiceStore, InvoiceStore>();
            services.AddTransient<IFileHandler, FileHandler>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWin = ServiceProvider.GetService<MainWindow>();
            mainWin.Show();
        }
        
    }
}
