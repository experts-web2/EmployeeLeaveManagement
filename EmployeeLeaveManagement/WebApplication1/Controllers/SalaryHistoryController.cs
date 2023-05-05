using BL.Interface;
using DAL.Interface;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SalaryHistoryController : ControllerBase
    {
        private readonly ISalaryHistoryRepository repositroy;
        private readonly ISalaryService _salaryService;
        public SalaryHistoryController(ISalaryHistoryRepository _repository,ISalaryService salaryService)
        {
            repositroy= _repository;
            _salaryService=salaryService;
        }
        [HttpPost("AddSalary")]
        public IActionResult AddSalary(SalaryHistoryDto salaryDto)
        {
            if(salaryDto!=null)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var ClaimRoleId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (salaryDto.EmployeeId is null && ClaimRoleId is not null && int.TryParse(ClaimRoleId, out int RoleID) && RoleID > 0)
                    salaryDto.EmployeeId = RoleID;
                _salaryService.AddSalary(salaryDto);
                return Ok();
            }
           return BadRequest();
        }
        [HttpPost("GetSalaries")]
        public IActionResult GetSalaries(Pager pager)
        {
                var allSalaries = _salaryService.GetSalaries(pager);
                var metadata = new
                {
                    allSalaries.TotalCount,
                    allSalaries.PageSize,
                    allSalaries.TotalPages,
                    allSalaries.CurrentPage,
                    allSalaries.HasPrevious,
                    allSalaries.HasNext,
                    pager.Search
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                if (allSalaries != null) return Ok(allSalaries);
                return BadRequest();
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetSalaryById(int id)
        {
            var response = _salaryService.GetSalaryById(id);
            if (response != null) return Ok(response);
            return BadRequest();
        }
        [HttpPost("GetSalariesForUser/{id}")]
        public IActionResult GetSalariesForUser(int id)
        {
            var response = _salaryService.GetSalary(id);
            if (response != null)
                return Ok(response);
            return BadRequest();
        }
        [HttpPut("EditSalary")]
        public IActionResult EditSalary(SalaryHistoryDto salaryDto)
        {
            if(salaryDto!=null)
            {
                _salaryService.EditSalary(salaryDto);
                return Ok("Updated successfull");
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSalary(int id)
        {
            _salaryService.DeleteSalary(id);
            return Ok("Deleted Successfull");
        }
    }
}
