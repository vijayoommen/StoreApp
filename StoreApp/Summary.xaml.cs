using StoreApp.UI.DataAccess;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.ObjectModel;

namespace StoreApp.UI
{
    /// <summary>
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : Window
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IInvoiceStore invoiceStore;
        public ObservableCollection<InvoiceStore.SalesSummary> invoiceSummary;

        public Summary(IServiceProvider serviceProvider, IInvoiceStore invoiceStore)
        {
            InitializeComponent();
            this.DataContext = this;

            this.serviceProvider = serviceProvider;
            this.invoiceStore = invoiceStore;

            invoiceSummary = new ObservableCollection<InvoiceStore.SalesSummary>();

            Refresh();
            
            listInvoiceSummary.ItemsSource = invoiceSummary;
        }

        private void Refresh()
        {
            var summary = this.invoiceStore.GetInvoiceSummary();
            
            invoiceSummary.Clear();

            foreach (var item in summary)
            {
                invoiceSummary.Add(item);
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            serviceProvider.GetService<MainWindow>().Show();

            this.Visibility = Visibility.Hidden;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
