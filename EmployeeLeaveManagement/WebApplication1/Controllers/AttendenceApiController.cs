using DAL.Interface;
using DTOs;
using ELM.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceApiController : ControllerBase
    {
      private  IAttendenceRepository attendenceRepository;
        public AttendenceApiController(IAttendenceRepository _attendenceRepository)
        {
            attendenceRepository= _attendenceRepository;
        }
        [HttpPost("AddAttendence")]
        public IActionResult AddAttendence([FromBody]AttendenceDto attendenceDto)
        {
            if (ModelState.IsValid)
            {
                var response = attendenceRepository.AddAttendence(attendenceDto);
                return Ok("Added Successfull");
            }
            else return BadRequest("Unable to Add");
        }
        [HttpPost("GetAllAttendences")]
        public IActionResult GetAllAttendences(Pager paging)
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
                paging.StartDate,
                paging.EndDate,
                paging.Search
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (allAttendences != null)
                return Ok(allAttendences);
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAttendence(int id)
        {
            bool response =attendenceRepository.DeleteAttendence(id);
            if (response=false)
                return BadRequest("Unable to Delete");
            return Ok("Deleted Succesfully");
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
            var attendenceDto = attendenceRepository.GetById(id);
            if (attendenceDto != null)
                return Ok(attendenceDto);
            else
                return BadRequest("Unable to get Attendence");
        }
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
