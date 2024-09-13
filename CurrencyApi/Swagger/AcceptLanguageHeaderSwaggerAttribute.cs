using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CurrencyApiLib.Swagger;

/// <summary>
/// The accept language header swagger attribute.
/// </summary>
public class AcceptLanguageHeaderSwaggerAttribute : IOperationFilter
{

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //operation.Parameters ??= new List<OpenApiParameter>();

        //operation.Parameters.Add(new OpenApiParameter
        //{
        //    //Name = "Accept-Language",
        //    //In = ParameterLocation.Header,
        //    //Required = false,
        //    //Schema = new OpenApiSchema
        //    //{
        //    //    Type = "string"
        //    //}
        //});
    }

}