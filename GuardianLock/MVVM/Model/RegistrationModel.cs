using GuardianLock.Helper;
using GuardianLock.Helper.DAL;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace GuardianLock.MVVM.Model
{    
    /// <summary>
    /// Contains the logic for registering a user and saving their information.
    /// </summary>
    public static class RegistrationModel
    {
        /// <summary>
        /// Saves the user's information in the database. Ch
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        public static bool SaveRegistrationInfo(string username, string password)
        {
            try
            {
                object userExists = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = '" + username + "'");
                if (userExists is not null)
                {
                    MessageBox.Show("A user with that username already exists. Please try again with a different username.");
                    return false;
                }

                if (!Encryption.EnsurePasswordIsSecure(password))
                {
                    MessageBox.Show("Your password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
                    return false;
                }

                string userId = Encryption.GenerateUserID();
                string hashedPassword = Encryption.HashPassword(password, out byte[] salt);
                string encryptionKey = Encryption.GenerateEncryptionKey();

                string query = $"INSERT INTO Users (UserID, Username, PasswordHash, Salt, EncryptionKey) VALUES ('{userId}', '{username}', '{hashedPassword}', '{Convert.ToHexString(salt)}', '{encryptionKey}')";

                DAL.ExecuteQuery(query);

                object potentialUser = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = '" + username + "'");
                if (potentialUser is not null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }
    }
}
