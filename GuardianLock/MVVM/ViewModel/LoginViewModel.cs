using GuardianLock.Core;
using System.Windows;
using System.Security;
using System.Net;
using GuardianLock.Helper.DAL;
using System.Data.SqlClient;
using BCrypt.Net;

namespace GuardianLock.MVVM.ViewModel
{
    /// <summary>
    /// Represents the view model for the login functionality.
    /// </summary>
    class LoginViewModel : ObservableObject
    {

        public string username;
        public SecureString password;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
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
        /// Gets the command for the login functionality.
        /// </summary>
        public RelayCommand LoginCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        /// <summary>
        /// Determines whether the login command can be executed.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        /// <returns><c>true</c> if the login command can be executed; otherwise, <c>false</c>.</returns>
        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && Password != null && Password.Length > 0;
        }

        /// <summary>
        /// Executes the login command.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void Login(object parameter)
        {
            if (IsValidCredentials(username, password))
            {
                MainWindow mainWindow = new();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Login)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Validates the user credentials.
        /// </summary>
        /// <returns><c>true</c> if the credentials are valid; otherwise, <c>false</c>.</returns>
        private static bool IsValidCredentials(string username, SecureString password)
        {
            // Use parameterized queries to avoid SQL injection
            object userNameExists = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = @Username", new SqlParameter("@Username", username));

            if (userNameExists == null)
            {
                MessageBox.Show("Username does not exist.");
                return false;
            }

            string passwordString = new NetworkCredential(string.Empty, password).Password;
            string passwordHash = DAL.ExecuteQuery("SELECT PasswordHash FROM Users WHERE Username = @Username", new SqlParameter("@Username", username)).ToString();

            if (BCrypt.Net.BCrypt.Verify(passwordString, passwordHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
