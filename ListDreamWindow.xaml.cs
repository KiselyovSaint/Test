using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SleepSoftware.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListDreamWindow.xaml
    /// </summary>
    public partial class ListDreamWindow : Window
    {
        public ObservableCollection<MyDataItem> TestData { get; set; }
        public ListDreamWindow()
        {
            InitializeComponent();
            var testData = new List<MyDataItem>();

            testData.Add(new MyDataItem
            {
                Date1 = $"16.05.2024 23:00",
                Date2 = $"17.05.2024 7:00",
                Text = $"Отличный сон"
            });
            testData.Add(new MyDataItem
            {
                Date1 = $"17.05.2024 23:00",
                Date2 = $"28.05.2024 4:00",
                Text = $"Плохой сон"
            });
            testData.Add(new MyDataItem
            {
                Date1 = $"20.05.2024 00:00",
                Date2 = $"20.05.2024 7:00",
                Text = $"Хороший сон"
            });

            // Установка тестовых данных в качестве ItemsSource для ListView
            lwTest.ItemsSource = testData;
        }
        public class MyDataItem
        {
            public string Date1 { get; set; }
            public string Date2 { get; set; }
            public string Text { get; set; }
        }
    }
    
}
