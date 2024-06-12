using SleepSoftware.ClassHelper;
using SleepSoftware.DB;
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
    /// Логика взаимодействия для Reg2Window.xaml
    /// </summary>
    public partial class Reg2Window : Window
    {
        private RegistrationData _registrationData;
        private RegWindow _previousWindow;

        public Reg2Window(RegistrationData registrationData, RegWindow previousWindow)
        {
            InitializeComponent();
            _registrationData = registrationData;
            _previousWindow = previousWindow;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _previousWindow.Show();
            this.Close();
        }

        private void pwPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtWaterPassword.Visibility = Visibility.Collapsed;
        }

        private void pwPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pwPassword.Password))
            {
                txtWaterPassword.Visibility = Visibility.Visible;
            }
        }

        private void pwRepeatPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtWaterRepeatPassword.Visibility = Visibility.Collapsed;
        }

        private void pwRepeatPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pwRepeatPassword.Password))
            {
                txtWaterRepeatPassword.Visibility = Visibility.Visible;
            }
        }

        private void tbxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbxLogin.Foreground == Brushes.Gray)
            {
                tbxLogin.Text = "";
                tbxLogin.Foreground = Brushes.Black;
            }
        }

        private void tbxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxLogin.Text))
            {
                tbxLogin.Text = "Придумайте логин";
                tbxLogin.Foreground = Brushes.Gray;
            }
        }

        private void RegIn_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, есть ли похожий логин в базе данных
            string login = tbxLogin.Text.Trim();

            if (EFClass.context.User.Any(u => u.Login == login))
            {
                MessageBox.Show("Логин уже существует. Выберите другой логин.");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbxLogin.Text) || tbxLogin.Foreground == Brushes.Gray)
            {
                MessageBox.Show("Укажите логин.");
                return;
            }

            if (string.IsNullOrWhiteSpace(pwPassword.Password) || string.IsNullOrWhiteSpace(pwRepeatPassword.Password))
            {
                MessageBox.Show("Придумайте пароль и повторите его.");
                return;
            }

            // Проверяем, совпадают ли пароли
            if (pwPassword.Password != pwRepeatPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            // Создаем нового пользователя
            User user = new User();
            user.Login = tbxLogin.Text;
            user.Password = pwPassword.Password;

            EFClass.context.User.Add(user);
            EFClass.context.SaveChanges();

            // Создаем профиль для нового пользователя
            Profile profile = new Profile();
            profile.FName = _registrationData.firstName;
            profile.LName = _registrationData.lastName;
            profile.Birthday = _registrationData.birthDay;
            profile.IDGender = _registrationData.gender;
            profile.IDUser = user.ID;

            EFClass.context.Profile.Add(profile);
            EFClass.context.SaveChanges();

            MessageBox.Show("Регистрация успешна!");
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
