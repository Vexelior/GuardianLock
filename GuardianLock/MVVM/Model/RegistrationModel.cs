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
        public static bool SaveRegistrationInfo(string email, string firstName, string lastName, string password)
        {
            try
            {
                object userExists = DAL.ExecuteQuery("SELECT * FROM Users WHERE Email = @Email", new SqlParameter("@Username", email));
                if (userExists is not null)
                {
                    MessageBox.Show("A user with that email already exists. Please try again with a different username.", "Already Exists", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    new("@Email", email),
                    new("@FirstName", firstName),
                    new("@LastName", lastName),
                    new("@PasswordHash", hashedPassword),
                    new("@Salt", salt),
                    new("@EncryptionKey", encryptionKey)
                ];

                string query = $"INSERT INTO Users (UserID, Email, FirstName, LastName, PasswordHash, Salt, EncryptionKey) VALUES (@UserID, @Email, @FirstName, @LastName, @PasswordHash, @Salt, @EncryptionKey)";

                DAL.ExecuteQuery(query, parameters);

                object potentialUser = DAL.ExecuteQuery("SELECT * FROM Users WHERE Email = @Email", new SqlParameter("@Email", email));
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
