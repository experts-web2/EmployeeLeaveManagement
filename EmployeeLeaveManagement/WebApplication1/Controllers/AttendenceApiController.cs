using BL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AttendenceApiController : ControllerBase
    {
        private IAttendenceService _attendenceService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AttendenceApiController(UserManager<User> userManager, SignInManager<User> signInManager, IAttendenceService attendenceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _attendenceService = attendenceService;
        }
        [HttpPost("AddAttendence")]
        public IActionResult AddAttendence([FromBody] AttendenceDto attendenceDto)
        {
            if (ModelState.IsValid)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var ClaimRoleId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (attendenceDto.EmployeeId is null && ClaimRoleId is not null && int.TryParse(ClaimRoleId, out int RoleID) && RoleID > 0)
                    attendenceDto.EmployeeId = RoleID;
                var response = _attendenceService.AddAttendence(attendenceDto);
                return Ok("Added Successfull");
            }
            else return BadRequest("Unable to Add");
        }
        public static string AccessToken;

        [HttpPost("GetAllAttendences")]
        public async Task<IActionResult> GetAllAttendences(Pager paging)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Role = identity?.FindFirst(ClaimTypes.Role);
            var ClaimRoleId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Role.Value.Contains("Admin"))
            {
                return GetAllEmployeeAttendance(paging);
            }


            else
                return GetAttendencesByEmployeeId(int.Parse(ClaimRoleId));

        }
        [HttpGet]
        private IActionResult GetAllEmployeeAttendance(Pager paging)
        {
            var allAttendences = _attendenceService.GetAllAttendences(paging);
            var metadata = new
            {
                allAttendences.TotalCount,
                allAttendences.PageSize,
                allAttendences.TotalPages,
                allAttendences.CurrentPage,
                allAttendences.HasPrevious,
                allAttendences.HasNext,
                paging.Search
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (allAttendences != null)
                return Ok(allAttendences);
            return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteAttendence(int id)
        {
            bool response = _attendenceService.DeleteAttendence(id);
            if (response == false)
                return BadRequest("Unable to Delete");
            return Ok("Deleted Succesfully");
        }
        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery]int id)
        {
            var attendenceDto = _attendenceService.GetById(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }
        [HttpGet]
        public IActionResult GetAttendencesByEmployeeId(int id)
        {
            var attendenceDto = _attendenceService.GetAttendencesByEmployeeId(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }

        [HttpGet("GetAttendenceByEmployeeId")]
        public IActionResult GetAttendenceByEmployeeId([FromQuery] int id)
        {
            var attendenceDto = _attendenceService.GetAttendenceByEmployeeId(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }
        [HttpPut]
        public IActionResult UpdateAttendence(AttendenceDto attendenceDto)
        {
            bool response = _attendenceService.UpdateAttendence(attendenceDto);
            if (response == false)
                return BadRequest("Unable to Update Attendence");
            return Ok("Update Successfull");
        }
    }
}
