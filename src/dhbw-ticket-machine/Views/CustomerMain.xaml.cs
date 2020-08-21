using dhbw_ticket_machine.Views.CustomerViews;
using System;
using System.Collections.Generic;
using System.Linq;
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
            mainFrame.NavigationService.Navigate(new SelectCustomer());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<MainWindow>().Any())
            {
                var window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                if(window.WindowState == WindowState.Minimized)
                {
                    window.WindowState = WindowState.Normal;
                }
                window.Activate();
            }
            else
            {
                var window = new MainWindow();
                window.Show();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(Application.Current.Windows.Count < 2)
            {
                var msg = MessageBox.Show("Die Anwendung wird beendet. Alle geänderten Daten gehen verloren!\n\n Fortfahren?", "Warnung!", MessageBoxButton.YesNo);
                if (msg == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else if (msg == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
        }
    }
}
