using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using System.Xml.Linq;

namespace DataGrid.ViewModels
{
    public class Person : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string AgeGrade
        {
            get
            {
                if (Age >= 65) return "노년";
                if (Age >= 40) return "중년";
                return "청년";
            }
        }

        //////////////////////////////////////////////////////////

        // IDataErrorInfo 구현
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Age))
                {
                    if (Age < 0 || Age > 120)
                        return "나이는 0~120 사이여야 합니다.";
                }

                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrWhiteSpace(Name))
                        return "이름을 입력해주세요.";
                }

                return null;
            }
        }
    }
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AgeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int age) 
            {
                return age >= 60 ? Brushes.LightCoral : Brushes.White;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DataGridViewModel : ObservableObject
    {
        public ObservableCollection<Person> People { get; }

        public DataGridViewModel()
        {
            People = new ObservableCollection<Person>();
            for (int i = 1; i <= 5000; i++)  // 👈 수만 건으로 테스트
            {
                People.Add(new Person
                {
                    Id = i,
                    Name = NameOptions[i % NameOptions.Count],
                    Age = (i % 100) + 1
                });
            }
            /*
            People = new ObservableCollection<Person>
            {
                new Person { Id = 1, Name = "Alice", Age = 28 },
                new Person { Id = 2, Name = "Bob", Age = 34 },
                new Person { Id = 3, Name = "Charlie", Age = 23 },
            };
            */

            FilteredPeople = CollectionViewSource.GetDefaultView(People);
            FilteredPeople.Filter = FilterByName;
            DeleteCommand = new DelegateCommand(OnDelete, CanDelete).ObservesProperty(() => SelectedPerson);
        }

        //////////////////////////////////////////////////////////
        public ObservableCollection<string> NameOptions { get; } = new ObservableCollection<string>
            {
                "Alice", "Bob", "Charlie", "Diana", "Eve"
            };


        //////////////////////////////////////////////////////////

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        public ICommand DeleteCommand { get; }
        private void OnDelete() => People.Remove(SelectedPerson);
        private bool CanDelete() => SelectedPerson != null;

        //////////////////////////////////////////////////////////

        public ICollectionView FilteredPeople { get; }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilteredPeople.Refresh();
            }
        }


        private bool FilterByName(object obj)
        {
            if (obj is Person person) {
                return string.IsNullOrWhiteSpace(SearchText)
                    || person.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        //////////////////////////////////////////////////////////

    }
}
