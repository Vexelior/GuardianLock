using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianLock.Core
{
    public class UserContext
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string EncryptionKey { get; set; }
    }
}
