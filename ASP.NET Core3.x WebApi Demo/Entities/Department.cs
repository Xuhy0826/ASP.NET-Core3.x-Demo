using System;
using System.Collections.Generic;

namespace ASP.NET_Core3.x_WebApi_Demo.Entities
{
    /// <summary>
    /// “部门”实体类
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门下员工（导航属性）
        /// </summary>
        public ICollection<Employee> Employees { get; set; }
    }
}
