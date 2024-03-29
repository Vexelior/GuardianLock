﻿namespace GuardianLock.Core
{
    /// <summary>
    /// Represents the context of a user.
    /// </summary>
    public class UserContext
    {
        /// <summary>
        /// Gets or sets the ID of the user.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the encryption key of the user.
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}
