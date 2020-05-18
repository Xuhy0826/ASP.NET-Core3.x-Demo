using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using Demo.Dto.Dtos;

namespace ASP.NET_Core3.x_WebApi_Demo.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(EmployeeQueryDto queryParams);
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<bool> ExistsAsync(Guid employeeId);
        Task<bool> SaveAsync();
    }
}
