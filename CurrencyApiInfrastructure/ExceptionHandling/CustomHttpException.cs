using System;
using System.Net;

namespace CurrencyApiInfrastructure.ExceptionHandling
{
    /// <summary>
    /// The custom http exception.
    /// </summary>
    public abstract class CustomHttpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHttpException"/> class.
        /// </summary>
        /// <param name="Message">The message.</param>
        public CustomHttpException(string Message)
       : base(Message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHttpException"/> class.
        /// </summary>
        public CustomHttpException()
            : this("Internal Server Error!")
        {

        }

        /// <summary>
        /// Gets the http status code.
        /// </summary>
        public abstract HttpStatusCode HttpStatusCode { get; }
    }
}
