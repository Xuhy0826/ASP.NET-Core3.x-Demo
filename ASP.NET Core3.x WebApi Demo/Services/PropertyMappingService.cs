using System;
using System.Collections.Generic;
using System.Linq;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using Demo.Dto.Dtos;
using Mark.Common.Services;

namespace ASP.NET_Core3.x_WebApi_Demo.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {nameof(EmployeeDto.Id), new PropertyMappingValue(new List<string>{nameof(Employee.Id)}) },
                {nameof(EmployeeDto.Name), new PropertyMappingValue(new List<string>{nameof(Employee.Name) }) },
                {nameof(EmployeeDto.DepartmentId), new PropertyMappingValue(new List<string>{nameof(Employee.DepartmentId) }) },
                {nameof(EmployeeDto.EmployeeNo), new PropertyMappingValue(new List<string>{nameof(Employee.EmployeeNo) }) },
                {nameof(EmployeeDto.GenderDes), new PropertyMappingValue(new List<string>{nameof(Employee.Gender)}) },
                {nameof(EmployeeDto.Age), new PropertyMappingValue(new List<string>{nameof(Employee.Id)}, true)}
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count == 1)
            {
                return propertyMappings.First().MappingDictionary;
            }

            throw new Exception($"无法找到唯一的映射关系：{typeof(TSource)}, {typeof(TDestination)}");
        }

        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }
            var fieldAfterSplit = fields.Split(",");
            return (from field in fieldAfterSplit
                    select field.Trim()
                into trimmedField
                    let indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal)
                    select indexOfFirstSpace == -1
                        ? trimmedField
                        : trimmedField.Remove(indexOfFirstSpace)).All(propertyName => propertyMapping.ContainsKey(propertyName));
        }
    }
}
