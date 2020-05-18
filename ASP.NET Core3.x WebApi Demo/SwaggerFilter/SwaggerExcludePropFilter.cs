using System.Linq;
using System.Reflection;
using Demo.Dto.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ASP.NET_Core3.x_WebApi_Demo.SwaggerFilter
{
public class SwaggerExcludePropFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.DeclaringType == null) return;
        var parms = context.MethodInfo.GetParameters();
        foreach (var parameterInfo in parms)
        {
            foreach (var property in parameterInfo.ParameterType.GetProperties())
            {
                var excludeAttributes = property.GetCustomAttribute<SwaggerExcludeAttribute>();
                if (excludeAttributes == null) continue;
                var prop = operation.Parameters.FirstOrDefault(p => p.Name == property.Name);
                if (prop != null)
                {
                    operation.Parameters.Remove(prop);
                }
            }
        }
    }
}
}
