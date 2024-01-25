using GuardianLock.MVVM.Model;
using GuardianLock.MVVM.ViewModel;
using System.Security;
using System.Windows;
using System.Windows.Controls;
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

        //private void BtnReg_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string username = this.UsernameTextBox.Text;
        //        string password = this.PasswordBox.Password;
        //        string confirmPassword = this.ConfirmPasswordBox.Password;

        //        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        //        {
        //            MessageBox.Show("All fields are required.", "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        RegistrationLogic.SaveInfo(username, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Something went wrong, Please try again later. - {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

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
