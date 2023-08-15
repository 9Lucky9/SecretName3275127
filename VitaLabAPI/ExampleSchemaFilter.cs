using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VitaLabData.DTOs;
using VitLabData.DTOs.Create;

namespace VitaLabAPI
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(UserCreateRequest))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Name"] = new OpenApiString("Pavel Evstigneev"),
                    ["Login"] = new OpenApiString("Lucky"),
                    ["Password"] = new OpenApiString("1234"),
                };
            }
            if(context.Type == typeof(UserLoginRequest))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Login"] = new OpenApiString("Lucky"),
                    ["Password"] = new OpenApiString("1234"),
                };
            }
        }
    }
}
