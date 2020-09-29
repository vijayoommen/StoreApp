using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.UI.DataAccess;
using StoreApp.UI.Domain;
using System.Collections.ObjectModel;

namespace StoreApp.UI
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public ObservableCollection<LineItem> lineItems;
        private Invoice invoice;
        private IServiceProvider serviceProvider;
        private readonly IProductStore productStore;
        private readonly IInvoiceStore invoiceStore;
        private readonly IPromoCalculatorFactory priceCalculator;

        public Sales(IServiceProvider serviceProvider, IProductStore productStore, IInvoiceStore invoiceStore, IPromoCalculatorFactory priceCalculator)
        {
            InitializeComponent();
            this.DataContext = this;

            lineItems = new ObservableCollection<LineItem>();
            listLineItems.ItemsSource = lineItems;

            invoice = new Invoice();

            this.serviceProvider = serviceProvider;
            this.productStore = productStore;
            this.invoiceStore = invoiceStore;
            this.priceCalculator = priceCalculator;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // go back to master
            serviceProvider.GetService<MainWindow>().Show();

            this.Visibility = Visibility.Hidden;
        }

        private void AddLineItem_Click(object sender, RoutedEventArgs e)
        {
            var sku = txtProductcode.Text;
            var units = 0;

            var p = productStore.GetProductBySku(sku);
            if (!int.TryParse(txtUnits.Text, out units) || p == null)
            {
                Clear();
                return;
            }

            var pricingContext = new PricingContext() { Quantity = units, UnitPrice = p.UnitPrice, Promotion = p.Promotion };
            var price = priceCalculator.GetCalculator(p.Promotion.PromotionType).Calculate(pricingContext);
            
            lineItems.Add(new LineItem() { ProductName = p.Name, Units = units, UnitPrice = p.UnitPrice, Discount = price.Description, TotalPrice = price.PromoPrice });

            invoice.AddLineItem(p.Name, units, p.UnitPrice, price.Description, price.PromoPrice);

            Clear();
        }

        private void SaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            invoiceStore.SaveInvoice(invoice);
            invoice = new Invoice();
            lineItems.Clear();
        }

        void Clear()
        {
            txtProductcode.Text = "";
            txtUnits.Text = "";
        }
    }

    public class LineItem
    {
        public string ProductName { get; set; }
        public int Units { get; set; }
        public double UnitPrice { get; set; }
        public string Discount { get; set; }
        public double TotalPrice { get; set; }
    }
}
