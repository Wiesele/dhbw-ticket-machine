using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace dhbw_ticket_machine.Extensions
{
    public static class ObservableCollection
    {
        public static void AddRange<T>(this ObservableCollection<T> say, IEnumerable<T> Items)
        {
            foreach (var item in Items)
            {
                say.Add(item);
            }
        }
    }
}
