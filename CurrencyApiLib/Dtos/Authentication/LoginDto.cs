namespace CurrencyApiLib.Dtos.Authentication
{
    /// <summary>
    /// The login data transfer object.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
