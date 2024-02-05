using BL.Interface;
using BL.Service;
using DAL.Interface;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DailyTaskController : ControllerBase
    {
        private readonly IDailyTaskService _dailyTaskService;
        public DailyTaskController(IDailyTaskService dailyTaskService)
        {
            _dailyTaskService = dailyTaskService;
        }

        [HttpPost]
        public IActionResult AddDailyTask([FromBody] DailyTaskDto dailyTask)
        {
            var response = _dailyTaskService.AddDailyTask(dailyTask);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetAllDailyTask()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claimRole = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (claimRole == "Admin")
            {
                var response = _dailyTaskService.GetAllDailyTask();
                if (response.Any())
                {
                    return Ok(response);
                }
            }
            else
            {
                var employeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(employeeId, out int EmployeeId))
                {
                    var response = _dailyTaskService.GetAllDailyTaskByEmployeeId(EmployeeId);
                    if (response.Any())
                    {
                        return Ok(response);
                    }
                }
               
            }
            return Ok(new List<DailyTaskDto>());
        }

    }
}
