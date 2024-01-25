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
    public partial class Login : Window
    {
        /// <summary>
        /// Represents the login window of the application.
        /// </summary>
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();

            Uri iconUri = new("pack://application:,,,/Images/logo.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        /// <summary>
        /// Event handler for the PasswordBox's PasswordChanged event.
        /// Updates the password in the view model when the password is changed.
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                var passwordBox = (PasswordBox)sender;
                viewModel.Password = ConvertToSecureString(passwordBox.Password);
            }
        }

        /// <summary>
        /// Event handler for the TextBlock's MouseLeftButtonDown event.
        /// Opens the registration window and closes the login window.
        /// </summary>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Register regWidow = new();
            Application.Current.MainWindow = regWidow;
            regWidow.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is Login)
                {
                    window.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Converts a string password to a SecureString.
        /// </summary>
        /// <param name="password">The password to convert.</param>
        /// <returns>A SecureString representation of the password.</returns>
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
