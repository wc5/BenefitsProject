using AutoMapper;
using BenefitsProject.Core.Domain.Contracts;
using BenefitsProject.Core.Domain.Entities;
using BenefitsProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenefitsProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EmployeeReadDto>>> List()
        {
            var employees = await _employeeService.GetAll_Async();

            return Ok(_mapper.Map<IReadOnlyList<EmployeeReadDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
        {
            var employee = await _employeeService.GetById_Async(id);
            
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeReadDto>(employee));
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Add(EmployeeCreateDto createDto)
        {
            try
            {
                var employee = await _employeeService.Add_Async(_mapper.Map<Employee>(createDto));

                return CreatedAtAction(nameof(GetById), new { id = employee.Id }, _mapper.Map<EmployeeReadDto>(employee));
            }

            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, EmployeeUpdateDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

            try
            {
                _ = await _employeeService.Update_Async(_mapper.Map<Employee>(updateDto));
            }

            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetById_Async(id);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.Delete_Async(employee);

            return NoContent();
        }
    }
}