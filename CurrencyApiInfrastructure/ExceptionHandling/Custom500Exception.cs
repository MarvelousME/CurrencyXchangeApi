using System.Net;

namespace CurrencyApiInfrastructure.ExceptionHandling
{
    /// <summary>
    /// The custom500 exception.
    /// </summary>
    public class Custom500Exception : CustomHttpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Custom500Exception"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public Custom500Exception(string Message)
          : base(Message)
        {

        }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        public override HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
    }
}
