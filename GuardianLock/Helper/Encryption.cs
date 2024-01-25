using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace GuardianLock.Helper
{ 
    /// <summary>
    /// Contains the logic for encrypting and decrypting data.
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// Generates a random encryption key.
        /// </summary>
        /// <returns>The generated encryption key.</returns>
        public static string GenerateEncryptionKey()
        {
            string encryptionKey = string.Empty;
            try
            {
                encryptionKey = Guid.NewGuid().ToString().Replace("-", "")[..16];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return encryptionKey;
        }

        /// <summary>
        /// Generates a random user ID.
        /// </summary>
        /// <returns>The generated user ID.</returns>
        public static string GenerateUserID()
        {
            string userID = string.Empty;
            try
            {
                userID = Guid.NewGuid().ToString().Replace("-", "")[..16];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return userID;
        }

        /// <summary>
        /// Ensures that the password is secure.
        /// </summary>
        /// <returns><c>true</c> if the password is secure; otherwise, <c>false</c>.</returns>
        public static bool EnsurePasswordIsSecure(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }

    }
}
