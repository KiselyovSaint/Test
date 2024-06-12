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
using System.Windows.Shapes;

namespace SleepSoftware.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddDreamWindow.xaml
    /// </summary>
    public partial class AddDreamWindow : Window
    {
        public AddDreamWindow()
        {
            InitializeComponent();

            for (int i = 0; i <= 23; i++)
            {
                CbHours.Items.Add(i.ToString());
                CbHoursEnd.Items.Add(i.ToString());
            }
            for (int i = 0; i <= 59; i++)
            {
                CbMinute.Items.Add(i.ToString());
                CbMinuteEnd.Items.Add(i.ToString());
            }
        }
    }
}
