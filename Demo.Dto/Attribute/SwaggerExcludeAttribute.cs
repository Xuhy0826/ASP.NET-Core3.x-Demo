using System;

namespace Demo.Dto.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute : System.Attribute
    {
    }
}
