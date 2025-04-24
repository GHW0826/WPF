using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataGrid.ViewModels
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class DataGridViewModel : ObservableObject
    {
        public ObservableCollection<Person> People { get; }

        public DataGridViewModel()
        {
            People = new ObservableCollection<Person>
            {
                new Person { Id = 1, Name = "Alice", Age = 28 },
                new Person { Id = 2, Name = "Bob", Age = 34 },
                new Person { Id = 3, Name = "Charlie", Age = 23 },
            };
        }
    }
}
