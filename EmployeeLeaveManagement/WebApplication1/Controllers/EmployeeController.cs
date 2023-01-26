using DAL.Interface;
using DomainEntity.Models;
using DomainEntity.Pagination;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpPost("getall")]
        public IActionResult GetAllEmployee(Pager pager)
        {
           var allemployee= _employeeRepository.GetAllEmployee(pager);
            var metadata = new
            {
                allemployee.TotalCount,
                allemployee.PageSize,
                allemployee.TotalPages,
                allemployee.CurrentPage,
                allemployee.HasPrevious,
                allemployee.HasNext,
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(allemployee);
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            _employeeRepository.AddEmployee(employeeDto);
            return Ok("Added Succesfully"); 
        }

        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeDto employee)
        {
            _employeeRepository.Update(employee);
            return Ok("Updated Succesfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return Ok("Deleted Succesfully");
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
           var employeeDto= _employeeRepository.GetById(id);
            return Ok(employeeDto);
        }

    }
}
