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
        public IActionResult AddSalary(int employeeId)
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

        [HttpGet]
        public IActionResult GetSalaries()
        {
            var allSalaries = _salaryService.GetAllSalary();
            if (allSalaries != null)
            {
                return Ok(allSalaries);
            }
            return BadRequest("No salaries available");
        }

    }
}
