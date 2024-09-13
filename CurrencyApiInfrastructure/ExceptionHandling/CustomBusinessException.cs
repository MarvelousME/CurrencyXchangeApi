using System.Net;


namespace CurrencyApiInfrastructure.ExceptionHandling
{
    /// <summary>
    /// The custom business exception.
    /// </summary>
    public class CustomBusinessException : CustomHttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomBusinessException"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public CustomBusinessException(string Message)
           : base(Message)
        {

        }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
