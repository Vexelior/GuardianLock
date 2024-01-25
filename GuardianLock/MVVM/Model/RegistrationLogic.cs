using GuardianLock.Helper.DAL;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace GuardianLock.MVVM.Model
{    
    /// <summary>
    /// Contains the logic for registering a user and saving their information.
    /// </summary>
    public static class RegistrationLogic
    {
        /// <summary>
        /// Saves the user's information in the database.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        public static void SaveRegistrationInfo(string username, string password)
        {
            try
            {
                string userId = GenerateUserID();
                string salt = HashPassword(password, out byte[] saltBytes);
                string hashedPassword = HashPassword(password, out byte[] passwordBytes);
                string encryptionKey = GenerateEncryptionKey();

                string query = $"INSERT INTO Users (UserID, Username, PasswordHash, Salt, EncryptionKey) VALUES ('{userId}', '{username}', '{hashedPassword}', '{salt}', '{encryptionKey}')";

                DAL.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
