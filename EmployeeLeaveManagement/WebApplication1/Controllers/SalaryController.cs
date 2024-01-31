using BL.Interface;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SalaryController : ControllerBase
    {
        private ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpPost]
        public IActionResult AddorUpdateSalary([FromBody]int employeeId)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var ClaimEmployeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (ClaimEmployeeId is not null && int.TryParse(ClaimEmployeeId, out int RoleID) && RoleID > 0)
            {
                _salaryService.AddSalaryorUpdate(employeeId);
                return Ok("AddedorUpdate");
            }
            return BadRequest();
        }

        [HttpPost("UpdateEmployeeSalary")]
        public IActionResult UpdateEmployeeSalary(SalaryDto salaryDto) 
        {
            var response = _salaryService.UpdateEmployeeSalary(salaryDto);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAllSalaries()
        {
            var allSalaries = _salaryService.GetAllSalary();
            if (allSalaries != null)
            {
                return Ok(allSalaries);
            }
            return BadRequest("No salaries available");
        }

        [HttpGet("GetEmployeeSalary/{employeeId}")]
        public IActionResult GetSalariesByEmployeeId(int employeeId)
        {
            var allSalaries = _salaryService.GetAllSalariesByEmployeeId(employeeId);
            if (allSalaries != null)
            {
                return Ok(allSalaries);
            }
            return BadRequest("No salaries available");
        }
    }
}
