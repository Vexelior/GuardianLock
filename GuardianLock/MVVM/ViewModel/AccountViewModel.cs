using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianLock.MVVM.ViewModel
{
    public class AccountViewModel
    {
        public string DisplayUserInfo => $"Welcome, {App.CurrentUser.Username}"!;
    }
}
