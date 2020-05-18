using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core3.x_WebApi_Demo.Data;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using ASP.NET_Core3.x_WebApi_Demo.Helpers;
using Demo.Dto.Dtos;
using Mark.Common.ExtensionMethod;
using Mark.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core3.x_WebApi_Demo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DemoDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public EmployeeRepository(DemoDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }
        /// <summary>
        /// 获取“员工”数据
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(EmployeeQueryDto queryParams)
        {
            if (queryParams == null)
            {
                throw new ArgumentNullException(nameof(queryParams));
            }
            var queryExpression = _context.Employees as IQueryable<Employee>;

            if (!string.IsNullOrWhiteSpace(queryParams.Name))
            {
                queryParams.Name = queryParams.Name.Trim();
                queryExpression = queryExpression.Where(x => x.Name == queryParams.Name);
            }
            if (!string.IsNullOrWhiteSpace(queryParams.EmployeeNo))
            {
                queryParams.EmployeeNo = queryParams.EmployeeNo.Trim();
                queryExpression = queryExpression.Where(x => x.EmployeeNo == queryParams.EmployeeNo);
            }
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                queryParams.SearchTerm = queryParams.SearchTerm.Trim();
                queryExpression = queryExpression.Where(x => x.Name.Contains(queryParams.SearchTerm) ||
                                                             x.EmployeeNo.Contains(queryParams.SearchTerm));
            }

            var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();

            queryExpression = queryExpression.ApplySort(queryParams.OrderBy, mappingDictionary);

            return await PagedList<Employee>.CreateAsync(queryExpression, queryParams.PageNumber, queryParams.PageSize);
        }
        /// <summary>
        /// 根据id获取员工信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == employeeId);
        }
        /// <summary>
        /// 增加“员工”
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.Id = Guid.NewGuid();
            _context.Employees.Add(employee);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="employee"></param>
        public void UpdateEmployee(Employee employee)
        {

        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Remove(employee);
        }

        public async Task<bool> ExistsAsync(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }
            return await _context.Employees.AnyAsync(x => x.Id == employeeId);
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
