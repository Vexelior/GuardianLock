namespace GuardianLock.MVVM.ViewModel
{
    public class AccountViewModel
    {
        /// <summary>
        /// Welcome message.
        /// </summary>
        public static string WelcomeMessage => $"Welcome, {App.CurrentUser.FirstName}!";

        /// <summary>
        /// Encryption key of the current user.
        /// </summary>
        public static string EncryptionKey => $"Encryption Key: {App.CurrentUser.EncryptionKey}";

        public AccountViewModel()
        {
        }
    }
}
