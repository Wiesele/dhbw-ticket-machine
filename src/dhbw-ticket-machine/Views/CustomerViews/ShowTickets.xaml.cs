using dhbw_ticket_machine.Actors;
using dhbw_ticket_machine.Models;
using dhbw_ticket_machine.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace dhbw_ticket_machine.Views.CustomerViews
{
    /// <summary>
    /// Interaktionslogik für ShowTickets.xaml
    /// </summary>
    public partial class ShowTickets : Page
    {
        CustomerMainViewModel vm;
        public ShowTickets(Customer selectedCustomer)
        {
            InitializeComponent();
            vm = new CustomerMainViewModel(selectedCustomer);
            this.DataContext = this.vm;
            this.UpdateBudget();
            AdministrationActor.EventDataChange += EventDataChange;
            AdministrationActor.CustomerDataChange += CustomerDataChange;
        }

        private void EventDataChange(object sender, IEnumerable<Event> events)
        {
            this.Dispatcher.Invoke(this.vm.LoadData);
        }

        private void CustomerDataChange(object sender, Customer customer)
        {
            this.Dispatcher.Invoke(this.UpdateBudget);
        }


        private async void UpdateBudget()
        {
            await this.vm.UpdateCustomerData();
            BudgetLabel.Content = this.vm.SelectedCustomer.Budget;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.vm.FilterEvents();
        }


        /// <summary>
        /// Source for sorting 
        /// https://stackoverflow.com/a/9746174
        /// https://docs.microsoft.com/de-de/dotnet/framework/wpf/controls/how-to-sort-a-gridview-column-when-a-header-is-clicked
        /// </summary>
        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(listView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
    }
}
