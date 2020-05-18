using System;

namespace Demo.Dto.Dtos
{
    public class EmployeeDto
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
        public string GenderDes { get; set; }
        /// <summary>
        /// 员工年龄
        /// </summary>
        public int Age { get; set; }
    }
}
