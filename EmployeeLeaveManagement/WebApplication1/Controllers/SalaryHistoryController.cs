using DAL.Interface;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryHistoryController : ControllerBase
    {
        private readonly ISalaryHistoryRepository repositroy;
        public SalaryHistoryController(ISalaryHistoryRepository _repository)
        {
            repositroy= _repository;
        }
        [HttpPost("AddSalary")]
        public IActionResult AddSalary(SalaryHistoryDto salaryDto)
        {
            if(salaryDto!=null)
            {
                 repositroy.AddSalary(salaryDto);
                return Ok();
            }
           return BadRequest();
        }
        [HttpGet("GetSalaries")]
        public IActionResult GetSalaries()
        {
            var response = repositroy.GetSalaries();
            if (response != null)
             return Ok(response);
            return BadRequest();
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GerSalary(int id)
        {
            var response = repositroy.GetSalary(id);
            if (response != null)
                return Ok(response);
            return BadRequest();
        }
        [HttpPut("EditSalary")]
        public IActionResult EditSalary(SalaryHistoryDto salaryDto)
        {
            if(salaryDto!=null)
            {
                repositroy.EditSalary(salaryDto);
                return Ok("Updated successfull");
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSalary(int id)
        {
            repositroy.DeleteSalary(id);
            return Ok("Deleted Successfull");
        }
    }
}
