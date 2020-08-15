using dhbw_ticket_machine.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dhbw_ticket_machine.Views
{
    /// <summary>
    /// Interaktionslogik für CustomerMain.xaml
    /// </summary>
    public partial class CustomerMain : Window
    {
        public CustomerMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new CustomerMainPage());
        }
    }
}
