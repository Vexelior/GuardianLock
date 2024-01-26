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
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public static UserContext CurrentUser { get; set; } = new UserContext();
    }
}
