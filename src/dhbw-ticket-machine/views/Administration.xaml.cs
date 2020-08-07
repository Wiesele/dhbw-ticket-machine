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

            this.vm.LoadData();
        }
    }
}
