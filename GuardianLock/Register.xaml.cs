using GuardianLock.MVVM.Model;
using GuardianLock.MVVM.ViewModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GuardianLock
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();

            Uri iconUri = new("pack://application:,,,/Images/logo.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
            {
                var passwordBox = (PasswordBox)sender;
                viewModel.Password = ConvertToSecureString(passwordBox.Password);
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterViewModel viewModel)
            {
                var passwordBox = (PasswordBox)sender;
                viewModel.ConfirmPassword = ConvertToSecureString(passwordBox.Password);
            }
        }

        /// <summary>
        /// Opens the login window and closes the register window.
        /// </summary>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login loginWindow = new();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Register)
                {
                    window.Close();
                    break;
                }
            }
        }

        private static SecureString ConvertToSecureString(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            return securePassword;
        }
    }
}
