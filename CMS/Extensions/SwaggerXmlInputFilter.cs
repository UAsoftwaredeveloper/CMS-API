namespace CMS.Extensions
{
    using Cms.Services.Filters;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SwaggerXmlInputFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null)
            {
                // Check if the "application/xml" content type is already present
                if (!operation.RequestBody.Content.ContainsKey("application/xml"))
                {
                    // Generate schema for the request body type
                    var xmlMediaType = new OpenApiMediaType
                    {
                        Schema = context.SchemaGenerator.GenerateSchema(context.MethodInfo.GetParameters()[0].ParameterType, context.SchemaRepository)
                    };

                    // Add the "application/xml" content type to the request body content
                    operation.RequestBody.Content.Add("application/xml", xmlMediaType);
                }
            }
        }
    }

}
