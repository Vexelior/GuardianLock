using GuardianLock.Core;
using System;

namespace GuardianLock.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand AccountViewCommand { get; set; }
        public RelayCommand MyFilesViewCommand { get; set; }


        public AccountViewModel AccountVM { get; set; }
        public MyFilesViewModel MyFilesVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

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
