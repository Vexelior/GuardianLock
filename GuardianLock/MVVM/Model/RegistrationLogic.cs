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
    public static class RegistrationLogic
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
                int userExists = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = '" + username + "'");
                if (userExists > 0)
                {
                    MessageBox.Show("A user with that username already exists. Please try again with a different username.");
                    return false;
                }

                if (!EnsurePasswordIsSecure(password))
                {
                    MessageBox.Show("Your password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
                    return false;
                }

                string userId = GenerateUserID();
                string salt = HashPassword(password, out byte[] saltBytes);
                string hashedPassword = HashPassword(password, out byte[] passwordBytes);
                string encryptionKey = GenerateEncryptionKey();

                string query = $"INSERT INTO Users (UserID, Username, PasswordHash, Salt, EncryptionKey) VALUES ('{userId}', '{username}', '{hashedPassword}', '{salt}', '{encryptionKey}')";

                DAL.ExecuteQuery(query);

                int potentialUser = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = '" + username + "'");
                if (potentialUser > 0)
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

        /// <summary>
        /// Hashes the given password using the PBKDF2 algorithm with SHA512 hashing.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The generated salt used for hashing.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        static string HashPassword(string password, out byte[] salt)
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2
            (
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );

            return Convert.ToHexString(hash);
        }

        /// <summary>
        /// Generates a random encryption key.
        /// </summary>
        /// <returns>The generated encryption key.</returns>
        static string GenerateEncryptionKey()
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
        static string GenerateUserID()
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
        static bool EnsurePasswordIsSecure(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }
    }
}
