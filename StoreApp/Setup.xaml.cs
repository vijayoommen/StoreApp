using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.UI.DataAccess;
using StoreApp.UI.Domain;

namespace StoreApp.UI
{
    /// <summary>
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        private int _selectedProductIndx = 0;
        private int _promoTypeSelected = 0;
        private int _percentageDiscount = 0;
        private int _bogoBuy;
        private int _bogoGet;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductStore _productstore;

        public Setup(IServiceProvider serviceProvider, IProductStore productstore)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _productstore = productstore;
        }

        private void cmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedProductIndx = cmbProduct.SelectedIndex;
        }

        private void PromoType_Checked(object sender, RoutedEventArgs e)
        {
            PercentOptions.Visibility = Visibility.Hidden;
            BogoOptions.Visibility = Visibility.Hidden;

            _promoTypeSelected = int.Parse(((RadioButton)e.Source).Tag.ToString());
            switch (_promoTypeSelected)
            {
                case 1:
                    PercentOptions.Visibility = Visibility.Visible;
                    break;
                case 2:
                    BogoOptions.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void PercentDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((TextBox)e.Source).Text, out int temp))
                _percentageDiscount = temp;
        }

        private void BogoBuy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((TextBox)e.Source).Text, out int temp))
                _bogoBuy = temp;
        }

        private void BogoGet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(((TextBox)e.Source).Text, out int temp))
                _bogoGet = temp;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // read data from 
            var product = _productstore.GetProductbyId(_selectedProductIndx);
            BasePromotion promo;

            switch (_promoTypeSelected)
            {
                case 1: promo = new PercentagePromotion() { PromotionPercentage = _percentageDiscount };
                    break;
                case 2: promo = new BogoPromotion() { Buy = _bogoBuy, Get = _bogoGet };
                    break;
                default: promo = new NonePromotion();
                    break;
            }

            product.Promotion = promo;
            _productstore.SaveProduct(product);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // go back to master
            _serviceProvider.GetService<MainWindow>().Show();

            this.Visibility = Visibility.Hidden;
        }
    }
}
