using SleepSoftware.ClassHelper;
using SleepSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.EFClass.context.Gender.ToList();
            cbGender.DisplayMemberPath = "NameGender";
        }

        private void tbxFName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxFName.Foreground == Brushes.Gray)
            {
                tbxFName.Text = "";
                tbxFName.Foreground = Brushes.Black;
            }
        }

        private void tbxFName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxFName.Text))
            {
                tbxFName.Text = "Имя";
                tbxFName.Foreground = Brushes.Gray;
            }
        }

        private void tbxLName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxLName.Foreground == Brushes.Gray)
            {
                tbxLName.Text = "";
                tbxLName.Foreground = Brushes.Black;
            }
        }

        private void tbxLName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxLName.Text))
            {
                tbxLName.Text = "Фамилия";
                tbxLName.Foreground = Brushes.Gray;
            }
        }

        private bool _focusSetProgrammatically = false;

        private void tbxWaterDate_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxWaterDate.Visibility= Visibility.Collapsed;
            _focusSetProgrammatically = true;
            DpBirthday.Focus();
        }

        private void DpBirthday_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!_focusSetProgrammatically && DpBirthday.SelectedDate == null)
            {
                tbxWaterDate.Visibility = Visibility.Visible;
            }
            _focusSetProgrammatically = false;
        }

        private void DpBirthday_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxWaterDate.Visibility = Visibility.Collapsed;
        }

        private void cbGender_GotFocus(object sender, RoutedEventArgs e)
        {
            txtWaterGender.Visibility = Visibility.Collapsed;
        }

        private void cbGender_DropDownClosed(object sender, EventArgs e)
        {
            if (cbGender.SelectedItem == null)
            {
                txtWaterGender.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow= new AuthWindow();
            authWindow.Show();
            Close();
        }

        private ClassHelper.RegistrationData _registrationData;
        private void btnConinuetReg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxFName.Text) || tbxFName.Foreground == Brushes.Gray)
            {
                MessageBox.Show("Заполните поле с именем.");
                return;
            }
            if (tbxFName.Text.Length > 30)
            {
                MessageBox.Show("Число символов в имени не должны быть больше 30.");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbxLName.Text) || tbxLName.Foreground == Brushes.Gray)
            {
                MessageBox.Show("Заполните поле с фамилией.");
                return;
            }

            if (tbxLName.Text.Length > 30)
            {
                MessageBox.Show("Число символов в фамилии не должны быть больше 30.");
                return;
            }
            if (DpBirthday.SelectedDate is null)
            {
                MessageBox.Show("Укажите дату рождения.");
                return;
            }
            if (cbGender.SelectedItem is null)
            {
                MessageBox.Show("Выберите пол.");
                return;
            }
            _registrationData = new RegistrationData
            {
                firstName = tbxFName.Text,
                lastName = tbxLName.Text,
                birthDay = DpBirthday.SelectedDate.Value,
                gender = cbGender.SelectedIndex + 1
            };

            Reg2Window reg2Window = new Reg2Window(_registrationData, this);
            reg2Window.Show();
            this.Hide();
        }
    }
}
