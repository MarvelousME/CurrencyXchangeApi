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
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : IBaseCheckService
    {
        private readonly T _service;
        private readonly IStringLocalizer<DisplayNameResource> _localizer;
        public GenericNotFoundFilter(T service, IStringLocalizer<DisplayNameResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

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
