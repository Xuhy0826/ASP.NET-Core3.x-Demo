using System;
using Demo.Dto.Enum;

namespace ASP.NET_Core3.x_WebApi_Demo.Entities
{
    /// <summary>
    /// “员工”实体类
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 关联的部门Id
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 员工性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 员工生日
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
    }
}
