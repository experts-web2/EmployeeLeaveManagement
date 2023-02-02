using DAL.Interface;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var allemployees = _employeeRepository.GetAllEmployee(pager);
                var metadata = new
                {
                    allemployees.TotalCount,
                    allemployees.PageSize,
                    allemployees.TotalPages,
                    allemployees.CurrentPage,
                    allemployees.HasPrevious,
                    allemployees.HasNext,
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(allemployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            var employeeDto = _employeeRepository.GetById(id);
            return Ok(employeeDto);
        }

    }
}
