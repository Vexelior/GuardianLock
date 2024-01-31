using GuardianLock.Core;
using System.Windows;
using System.Security;
using System.Net;
using GuardianLock.MVVM.Model;

namespace GuardianLock.MVVM.ViewModel
{
    /// <summary>
    /// Represents the view model for the register functionality.
    /// </summary>
    class RegisterViewModel : ObservableObject
    {

        public string email;
        public string firstName;
        public string lastName;
        public SecureString password;
        public SecureString confirmPassword;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        public SecureString ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        /// <summary>
        /// Gets the command for the login functionality.
        /// </summary>
        public RelayCommand RegisterCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class.
        /// </summary>
        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        /// <summary>
        /// Determines whether the register command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns><c>true</c> if the register command can be executed; otherwise, <c>false</c>.</returns>
        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrEmpty(Email) && 
                    Password != null && Password.Length > 0 && 
                    ConfirmPassword != null && ConfirmPassword.Length > 0;
        }

        /// <summary>
        /// Executes the register command.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void Register(object parameter)
        {
            if (IsValidCredentials(password, confirmPassword))
            {
                string passwordString = new NetworkCredential(string.Empty, password).Password;

                if (RegistrationModel.SaveRegistrationInfo(email, firstName, lastName, passwordString))
                {
                    MessageBox.Show("Registration successful!", "Register Success", MessageBoxButton.OK, MessageBoxImage.Information);

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
            }
            else
            {
                MessageBox.Show("Error with credentials. Please try again.", "Register Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <returns><c>true</c> if the credentials are valid; otherwise, <c>false</c>.</returns>
        private static bool IsValidCredentials(SecureString password, SecureString confirmPassword)
        {
            string passwordString = new NetworkCredential(string.Empty, password).Password;
            string confirmPasswordString = new NetworkCredential(string.Empty, confirmPassword).Password;

            if (confirmPasswordString != passwordString)
            {
                return false;
            }

            return true;
        }

        public static SecureString ConvertToSecureString(string password)
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
