using Akka.Actor;
using dhbw_ticket_machine.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dhbw_ticket_machine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Actorsystem of app
        /// </summary>
        public static ActorSystem ActorSystem { get; set; }

        public MainWindow()
        {
            MainWindow.ActorSystem =  ActorSystem.Create("Ticket-Machine");
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new MainMenu());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var msg = MessageBox.Show("Die gesamte Anwendung wird beendet! \n\n Fortfahren?", "Warnung!", MessageBoxButton.YesNo);
            if(msg == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
