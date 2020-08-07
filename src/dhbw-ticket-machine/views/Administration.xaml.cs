using dhbw_ticket_machine.Data;
using dhbw_ticket_machine.Models;
using dhbw_ticket_machine.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dhbw_ticket_machine.Views
{
    /// <summary>
    /// Interaktionslogik für Administration.xaml
    /// </summary>
    public partial class Administration : Page
    {
        AdministrationViewModel vm;
        public Administration()
        {
            this.vm = new AdministrationViewModel();
            InitializeComponent();
            DataContext = vm;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.vm.SaveNewEvent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm.LoadData();
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            var box = sender as ComboBox;
            box.IsDropDownOpen = false;
            this.vm.LoadSuggestions(box.Text);
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var box = sender as ComboBox;
            box.IsDropDownOpen = false;
        }
    }
}
