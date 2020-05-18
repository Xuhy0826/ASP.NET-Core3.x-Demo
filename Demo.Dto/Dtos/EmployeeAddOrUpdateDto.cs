using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Demo.Dto.Enum;

namespace Demo.Dto.Dtos
{
    public abstract class EmployeeAddOrUpdateDto : IValidatableObject
    {
        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}的长度是{1}")]
        public string EmployeeNo { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        public DateTime BirthDay { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthDay >= DateTime.Now)
            {
                yield return new ValidationResult("出生日期不能大于当前时间",
                    new[] { nameof(BirthDay) });
            }
        }
    }
}
