using System.Net;


namespace CurrencyApiInfrastructure.ExceptionHandling
{
    public class CustomBusinessException : CustomHttpException
    {
        public CustomBusinessException(string Message)
           : base(Message)
        {

        }

        public override HttpStatusCode HttpStatusCode => HttpStatusCode.BadRequest;
    }
}
