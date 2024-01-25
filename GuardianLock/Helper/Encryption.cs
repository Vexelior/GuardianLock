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
        /// The size of the key in bytes.
        /// </summary>
        private const int keySize = 64;
        /// <summary>
        /// The number of iterations to use for the PBKDF2 algorithm.
        /// </summary>
        private const int iterations = 350000;

        /// <summary>
        /// Hashes the given password using the PBKDF2 algorithm with SHA512 hashing.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The generated salt used for hashing.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        public static string HashPassword(string password, out byte[] salt)
        {
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

        // Write a method that verifies the supplied password against the stored hash.
        public static bool VerifyPassword(string password, byte[] storedSalt, string storedHash)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            // Use the stored salt during verification
            var newHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), storedSalt, iterations, hashAlgorithm, keySize);

            Debug.WriteLine(storedHash);
            Debug.WriteLine(Convert.ToHexString(newHash));

            // Compare the stored hash with the newly generated hash
            return Convert.ToHexString(newHash) == storedHash;
        }

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
