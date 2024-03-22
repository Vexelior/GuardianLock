using GuardianLock.Core;

namespace GuardianLock.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the AccountViewCommand.
        /// </summary>
        public RelayCommand AccountViewCommand { get; set; }
        /// <summary>
        /// Gets or sets the MyFilesViewCommand.
        /// </summary>
        public RelayCommand MyFilesViewCommand { get; set; }

        /// <summary>
        /// Gets or sets the AccountViewModel.
        /// </summary>
        public AccountViewModel AccountVM { get; set; }
        /// <summary>
        /// Gets or sets the MyFilesViewModel.
        /// </summary>
        public MyFilesViewModel MyFilesVM { get; set; }

        /// <summary>
        /// Current view selection property.
        /// </summary>
        private object _currentView;

        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class that displays what the user sees when they are logged in based on the CurrentView.
        /// </summary>
        public MainViewModel()
        {
            AccountVM = new AccountViewModel();
            MyFilesVM = new MyFilesViewModel();

            CurrentView = AccountVM;

            AccountViewCommand = new RelayCommand(o =>
            {
                CurrentView = AccountVM;
            });

            MyFilesViewCommand = new RelayCommand(o =>
            {
                CurrentView = MyFilesVM;
            });
        }
    }
}
