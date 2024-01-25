using System.Windows;
using System.Windows.Media.Imaging;

namespace GuardianLock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri iconUri = new("pack://application:,,,/Images/logo.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }
    }
}
