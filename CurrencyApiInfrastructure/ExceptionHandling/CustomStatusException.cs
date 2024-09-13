using System.Net;

namespace CurrencyApiInfrastructure.ExceptionHandling
{
    /// <summary>
    /// The custom status exception.
    /// </summary>
    public class CustomStatusException : CustomHttpException
    {
        /// <summary>
        /// Gets the status code.
        /// </summary>
        private int StatusCode { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomStatusException"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="StatusCode">The status code.</param>
        public CustomStatusException(string Message, int StatusCode)
         : base(Message)
        {
            this.StatusCode = StatusCode;
        }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        public override HttpStatusCode HttpStatusCode => (HttpStatusCode)StatusCode;
    }
}
