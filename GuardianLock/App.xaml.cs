using GuardianLock.Core;
using System.Configuration;
using System.Data;
using System.Windows;

namespace GuardianLock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserContext CurrentUser { get; set; } = new UserContext();
    }
}
