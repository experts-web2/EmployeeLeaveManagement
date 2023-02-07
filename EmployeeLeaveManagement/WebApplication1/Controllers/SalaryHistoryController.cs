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
    }
}
