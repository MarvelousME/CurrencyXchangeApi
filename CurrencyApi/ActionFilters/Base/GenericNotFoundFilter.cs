using CurrencyApiInfrastructure.BaseInterfaces;
using CurrencyApiInfrastructure.ExceptionHandling.Dtos;
using CurrencyApiInfrastructure.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CurrencyApiLib.ActionFilters.Base
{
    /// <summary>
    /// The generic not found filter.
    /// </summary>
    /// <typeparam name="T"/>
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : IBaseCheckService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private readonly T _service;
        /// <summary>
        /// The localizer.
        /// </summary>
        private readonly IStringLocalizer<DisplayNameResource> _localizer;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericNotFoundFilter"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="localizer">The localizer.</param>
        public GenericNotFoundFilter(T service, IStringLocalizer<DisplayNameResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        /// <summary>
        /// On action execution asynchronously.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="next">The next.</param>
        /// <returns>A Task</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionArgument = context.ActionArguments["dto"];
            Type modelType = actionArgument!.GetType();

            string modelIdPropName = modelType.GetProperties()
                                              .First(x => Attribute.IsDefined(x, typeof(KeyAttribute)))
                                              .Name;

            var id = (IConvertible)modelType.GetProperty(modelIdPropName)!
                                            .GetValue(actionArgument);

            bool doesExist = await _service.DoesEntityExistAsync(id);

            if (!doesExist)
            {
                string fieldName = modelType.GetProperty(modelIdPropName)!
                                            .GetCustomAttributes(false)
                                            .OfType<DisplayNameAttribute>()
                                            .First()
                                            .DisplayName;

                string localizedFieldName = _localizer[fieldName];
                string errorMesage = string.Format(_localizer["Display_Name_Not_Found"], localizedFieldName);
                var errorObject = new ErrorDto(errorMesage, (int)HttpStatusCode.NotFound);

                context.Result = new NotFoundObjectResult(errorObject);
                return;
            }
            else
            {
                await next();
            }
        }
    }
}
