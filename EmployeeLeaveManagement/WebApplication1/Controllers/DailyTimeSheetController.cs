using BL.Interface;
using BL.Service;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Principal;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyTimeSheetController : ControllerBase
    {
        private readonly IDailyTimeSheetService _dailyTimeSheetService;
        public DailyTimeSheetController(IDailyTimeSheetService dailyTimeSheetService)
        {
            _dailyTimeSheetService = dailyTimeSheetService;
        }

        [HttpGet]
        public IActionResult GetAllDailyTimeSheet()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claimRole = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (claimRole == "Admin")
            {
                var allDailySheets = _dailyTimeSheetService.GetAllDailyTimeSheet();
                if (allDailySheets.Any())
                {
                    return Ok(allDailySheets);
                }
            }
            else
            {
                var employeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(employeeId, out int EmployeeId))
                {
                    var response = _dailyTimeSheetService.GetAllDailyTimeSheetByEmployeeId(EmployeeId);
                    if (response.Any())
                    {
                        return Ok(response);
                    }
                }

            }
            return Ok(new List<DailyTimeSheetDto>());
        }

        [HttpPost]
        public IActionResult AddDailyTimeSheet(DailyTimeSheetDto dailyTimeSheetDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var employeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(employeeId, out int EmpId))
            {
                dailyTimeSheetDto.EmployeeId = EmpId;
                var allDailySheets = _dailyTimeSheetService.AddDailyTimeSheet(dailyTimeSheetDto);
                if (allDailySheets != null)
                {
                    return Ok(allDailySheets);
                }
            }
            return Ok(null);
        }

        [HttpPost("DailyTimeSheetwithFilter")]
        public IActionResult GetAllDailyTimeSheet(Pager paging)
        {
            var allDailyTimeSheets = _dailyTimeSheetService.GetAllDailyTimeSheetWithFilter(paging);
            var metadata = new
            {
                allDailyTimeSheets.TotalCount,
                allDailyTimeSheets.PageSize,
                allDailyTimeSheets.TotalPages,
                allDailyTimeSheets.CurrentPage,
                allDailyTimeSheets.HasPrevious,
                allDailyTimeSheets.HasNext,
                paging.Search
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (allDailyTimeSheets != null)
                return Ok(allDailyTimeSheets);
            return BadRequest();
        }

        [HttpPost("DailyTimeSheetwithFilterByEmpId/{EmpId}")]
        public IActionResult GetAllDailyTimeSheet(Pager paging, string EmpId)
        {
            var allDailyTimeSheets = _dailyTimeSheetService.GetAllDailyTimeSheetWithFilter(paging, EmpId);
            var metadata = new
            {
                allDailyTimeSheets.TotalCount,
                allDailyTimeSheets.PageSize,
                allDailyTimeSheets.TotalPages,
                allDailyTimeSheets.CurrentPage,
                allDailyTimeSheets.HasPrevious,
                allDailyTimeSheets.HasNext,
                paging.Search
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (allDailyTimeSheets != null)
                return Ok(allDailyTimeSheets);
            return BadRequest();
        }
    }
}
