using SleepSoftware.ClassHelper;
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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void btnMaximized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tbReg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }

        private void TbxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxLogin.Text))
            {
                TbxLogin.Visibility = Visibility.Collapsed;
                TbxWaterLogin.Visibility = Visibility.Visible;
            }
        }

        private void TbxWaterLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            TbxWaterLogin.Visibility = Visibility.Collapsed;
            TbxLogin.Visibility = Visibility.Visible;
            TbxLogin.Focus();
        }

        private void TbxWaterPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CbShowPassword.IsChecked == false)
            {
                TbxWaterPassword.Visibility = Visibility.Collapsed;
                pwbPassword.Visibility = Visibility.Visible;
                pwbPassword.Focus();
            }
            else
            {
                TbxWaterPassword.Visibility = Visibility.Collapsed;
                TbxShowPassword.Visibility = Visibility.Visible;
                TbxShowPassword.Focus();
            }
            
        }

        private void pwbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(pwbPassword.Password))
            {
                pwbPassword.Visibility = Visibility.Collapsed;
                TbxWaterPassword.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseOverTextBoxes(e.GetPosition(this)))
            {
                if (CbShowPassword.IsChecked == true)
                {
                    pwbPassword.Password = TbxShowPassword.Text;
                }
                else
                {
                    TbxShowPassword.Text = pwbPassword.Password;
                }
                RestoreWatermark();
                Keyboard.ClearFocus();
            }
        }

        private bool IsMouseOverTextBoxes(Point point)
        {
            return TbxLogin.InputHitTest(point) != null || TbxWaterLogin.InputHitTest(point) != null || pwbPassword.InputHitTest(point) != null || TbxWaterPassword.InputHitTest(point) != null || TbxShowPassword.InputHitTest(point) != null;
        }

        private void RestoreWatermark()
        {
            if (string.IsNullOrEmpty(TbxLogin.Text))
            {
                TbxLogin.Visibility = Visibility.Collapsed;
                TbxWaterLogin.Visibility = Visibility.Visible;
               
            }
            if (string.IsNullOrEmpty(pwbPassword.Password) && (string.IsNullOrEmpty(TbxShowPassword.Text)))
            {
                pwbPassword.Visibility = Visibility.Collapsed;
                TbxWaterPassword.Visibility = Visibility.Visible;
            }
            
        }

        private void CbShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (CbShowPassword.IsChecked == true)
            {
                TbxShowPassword.Text = pwbPassword.Password; // скопируем в TextBox из PasswordBox
                TbxShowPassword.Visibility = Visibility.Visible; // TextBox - отобразить
                pwbPassword.Visibility = Visibility.Hidden; // PasswordBox - скрыть
            }
            else
            {
                pwbPassword.Password = TbxShowPassword.Text; // скопируем в PasswordBox из TextBox 
                TbxShowPassword.Visibility = Visibility.Hidden; // TextBox - скрыть
                pwbPassword.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }

        private void TbxShowPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbxShowPassword.Text))
            {
                TbxShowPassword.Visibility = Visibility.Collapsed;
                TbxWaterPassword.Visibility = Visibility.Visible;
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            if (CbShowPassword.IsChecked == true)
            {
                pwbPassword.Password = TbxShowPassword.Text;
            }
            var authUser = EFClass.context.User.ToList()
               .Where(i => i.Login == TbxLogin.Text && i.Password == pwbPassword.Password).FirstOrDefault();

            if (authUser != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неравильно введён логин или пароль", "Неудачно");
            }
        }
    }
}
