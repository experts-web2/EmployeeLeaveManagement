using BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveHistoryController : ControllerBase
    {
        private IleaveHistoryService _leaveHistoryService;
        public LeaveHistoryController(IleaveHistoryService leaveHistoryService)
        {
            _leaveHistoryService = leaveHistoryService;
        }

        [HttpGet("LeaveHistoryofEmployee/{EmployeeId}")]
        public IActionResult GetLeaveHistoryByEmployeeId(int EmployeeId)
        {
            var result = _leaveHistoryService.GetLeaveHistoryByEmployeeId(EmployeeId);
            return Ok(result);
        }
    }
}
