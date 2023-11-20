using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiRedoc.Middleware
{
    public class DescriptionSchemaFilter : ISchemaFilter 
    {
        //Middleware for add Description in properties of the Entities
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var descriptionAttribute = property.GetCustomAttribute<DescriptionAttribute>();
                
                if (descriptionAttribute == null) continue;
                
                var propertyName = property.Name;
                
                var propertySchema = schema.Properties?[propertyName.ToLower()];

                if (propertySchema != null)
                    propertySchema.Description = descriptionAttribute.Description;
            }
        }
    }
}