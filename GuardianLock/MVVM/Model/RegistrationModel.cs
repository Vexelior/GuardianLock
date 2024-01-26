using GuardianLock.Helper;
using GuardianLock.Helper.DAL;
using System.Windows;
using System.Data.SqlClient;

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
                object userExists = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = @Username", new SqlParameter("@Username", username));
                if (userExists is not null)
                {
                    MessageBox.Show("A user with that username already exists. Please try again with a different username.", "User Already Exists", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (!Encryption.EnsurePasswordIsSecure(password))
                {
                    MessageBox.Show("Your password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                string userId = Encryption.GenerateUserID();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string encryptionKey = Encryption.GenerateEncryptionKey();

                SqlParameter[] parameters =
                [
                    new("@UserID", userId),
                    new("@Username", username),
                    new("@PasswordHash", hashedPassword),
                    new("@Salt", salt),
                    new("@EncryptionKey", encryptionKey)
                ];

                string query = $"INSERT INTO Users (UserID, Username, PasswordHash, Salt, EncryptionKey) VALUES (@UserID, @Username, @PasswordHash, @Salt, @EncryptionKey)";

                DAL.ExecuteQuery(query, parameters);

                object potentialUser = DAL.ExecuteQuery("SELECT * FROM Users WHERE Username = @Username", new SqlParameter("@Username", username));
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
