using BL.Interface;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }
        [HttpPost]
        public IActionResult AddLeave(LeaveDto leaveDto)
        {
           var response= _leaveService.Add(leaveDto);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GelAllLeaves()
        {
            var AllLeave = _leaveService.GetAll();
            return Ok(AllLeave);
        }
        [HttpPut]
        public IActionResult UpdateLeave(LeaveDto leaveDto)
        {
          var Updated=  _leaveService.Update(leaveDto);
            return Ok(Updated);
        }
        [HttpDelete("{id}")]
        public IActionResult Deleteleave(int id)
        {
            _leaveService.Delete(id);
            return Ok("Deleted Succusfully"); 
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
            var leaveDto = _leaveService.GetById(id);
            return Ok(leaveDto);
        }
    }
}
