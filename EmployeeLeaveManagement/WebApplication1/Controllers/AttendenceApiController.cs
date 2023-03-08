using DAL;
using DAL.Interface;
using DomainEntity.Enum;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AttendenceApiController : ControllerBase
    {
      private  IAttendenceRepository attendenceRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AttendenceApiController(IAttendenceRepository _attendenceRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            attendenceRepository= _attendenceRepository;
            _userManager= userManager;
            _signInManager = signInManager;
        }
        [HttpPost("AddAttendence")]
        public IActionResult AddAttendence([FromBody]AttendenceDto attendenceDto)
        {
            if (ModelState.IsValid)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var ClaimRoleId = identity?.Claims.FirstOrDefault(x=>x.Type==  ClaimTypes.NameIdentifier)?.Value;
                if (attendenceDto.EmployeeId < 1&&ClaimRoleId is not null&& int.TryParse(ClaimRoleId,out int RoleID )&&RoleID>0)
                    attendenceDto.EmployeeId =RoleID;
                var response = attendenceRepository.AddAttendence(attendenceDto);
                return Ok("Added Successfull");
            }
            else return BadRequest("Unable to Add");
        }
        public static string AccessToken;

        [HttpPost("GetAllAttendences")]
        public async  Task<IActionResult> GetAllAttendences(Pager paging)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var ClaimRoleId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

           // int RoleId = int.Parse(ClaimRoleId);

          // if(RoleId == 1)
          if(ClaimRoleId is  null)
                return GetAllEmployeeAttendance(paging);
            else
                return GetAttendencebyId(int.Parse(ClaimRoleId));

        }
        [HttpGet]
        private IActionResult GetAllEmployeeAttendance(Pager paging)
        {
            var allAttendences = attendenceRepository.GetAllAttendences(paging);
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
            bool response =attendenceRepository.DeleteAttendence(id);
            if (response=false)
                return BadRequest("Unable to Delete");
            return Ok("Deleted Succesfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
            var attendenceDto = attendenceRepository.GetById(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }
        [HttpGet]
        public IActionResult GetAttendencebyId(int id)
        {
            var attendenceDto = attendenceRepository.GetAttendencebyEmployeeId(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateAttendence(AttendenceDto attendenceDto)
        {
            bool response =attendenceRepository.Update(attendenceDto);
            if (response = false)
                return BadRequest("Unable to Update Attendence");
            return Ok("Update Successfull");
        }
    }
}
