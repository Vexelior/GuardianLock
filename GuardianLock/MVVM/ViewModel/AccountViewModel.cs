namespace GuardianLock.MVVM.ViewModel
{
    public class AccountViewModel
    {
        public static string WelcomeMessage => $"Welcome, {App.CurrentUser.Email}!";
    }
}
