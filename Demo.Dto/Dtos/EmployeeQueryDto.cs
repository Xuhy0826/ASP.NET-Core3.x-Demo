using Demo.Dto.Attribute;

namespace Demo.Dto.Dtos
{
    public class EmployeeQueryDto : QueryBase
    {
        /// <summary>
        /// 员工的工号
        /// </summary>
        public string EmployeeNo { get; set; }
        /// <summary>
        /// 员工的姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 员工的性别
        /// </summary>
        [SwaggerExclude]
        public string Gender { get; set; }
    }
}
