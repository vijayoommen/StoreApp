using System;
using System.Windows;

namespace StoreApp.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Setup _setupWindow;
        private Sales _salesWindow;
        private Summary _summary;

        public MainWindow(Setup setupWindow, Sales salesWindow, Summary summary)
        {
            InitializeComponent();

            _setupWindow = setupWindow;
            _salesWindow = salesWindow;
            _summary = summary;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Setup_Click(object sender, RoutedEventArgs e)
        {
            _setupWindow.Show();
            //this.Hide();
        }

        private void Store_Click(object sender, RoutedEventArgs e)
        {
            _salesWindow.Show();
            //this.Hide();
        }

        private void Summary_Click(object sender, RoutedEventArgs e)
        {
            _summary.Show();
            //this.Hide();
        }
    }
}
