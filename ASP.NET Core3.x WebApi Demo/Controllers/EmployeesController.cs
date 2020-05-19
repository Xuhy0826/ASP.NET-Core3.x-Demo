using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using ASP.NET_Core3.x_WebApi_Demo.Repository;
using AutoMapper;
using Demo.Dto.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ASP.NET_Core3.x_WebApi_Demo.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        /// <summary>
        /// 获取“员工”数据（分页）
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetEmployees))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromQuery] EmployeeQueryDto parameters)
        {
            var employees = await _employeeRepository.GetEmployeesAsync(parameters);

            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        /// <summary>
        /// 根据Id获取当个员工数据
        /// </summary>
        /// <param name="employeeId">员工Id</param>
        /// <returns></returns>
        [HttpGet("{employeeId}", Name = nameof(GetEmployee))]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid employeeId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<EmployeeDto>(employee);
            return Ok(dto);
        }
        /// <summary>
        /// 创建一个“员工”数据
        /// </summary>
        /// <param name="employeeAddDto"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateEmployee))]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody]EmployeeAddDto employeeAddDto)
        {
            var entity = _mapper.Map<Employee>(employeeAddDto);
            _employeeRepository.AddEmployee(entity);
            await _employeeRepository.SaveAsync();
            var returnDto = _mapper.Map<EmployeeDto>(entity);
            return CreatedAtRoute(nameof(GetEmployee), new { employeeId = returnDto.Id },
                returnDto);
        }

        /// <summary>
        /// 更新“员工”数据
        /// </summary>
        /// <param name="employeeId">“员工”ID</param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromRoute]Guid employeeId, [FromBody]EmployeeUpdateDto employee)
        {
            if (!await _employeeRepository.ExistsAsync(employeeId))
            {
                return NotFound();
            }
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(employeeId);
            _mapper.Map(employee, employeeEntity);
            _employeeRepository.UpdateEmployee(employeeEntity);
            await _employeeRepository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// 局部更新一个“员工”的数据
        /// </summary>
        /// <param name="employeeId">“员工”ID</param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployee([FromRoute]Guid employeeId,
            [FromBody]JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await _employeeRepository.ExistsAsync(employeeId))
            {
                return NotFound();
            }
            var employeeEntity = await _employeeRepository.GetEmployeeAsync(employeeId);
            var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employeeEntity);
            // 需要处理验证错误
            patchDocument.ApplyTo(dtoToPatch, ModelState);
            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(dtoToPatch, employeeEntity);
            _employeeRepository.UpdateEmployee(employeeEntity);
            await _employeeRepository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// 删除“员工”数据
        /// </summary>
        /// <param name="employeeId">员工Id（Guid）</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}", Name = nameof(DeleteEmployee))]
        public async Task<IActionResult> DeleteEmployee(Guid employeeId)
        {
            var entity = await _employeeRepository.GetEmployeeAsync(employeeId);

            if (entity == null)
            {
                return NotFound();
            }
            _employeeRepository.DeleteEmployee(entity);
            await _employeeRepository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// 控制器功能
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,PATCH,DELETE,OPTIONS");
            return Ok();
        }
    }
}
