using BL.Interface;
using DAL.Interface;
using DAL.Migrations;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }
        [HttpPost("GetAlerts")]
        public IActionResult GetAlert(Pager pager)
        {

            var Alerts = _alertService.GetAllAlert(pager);
            var metadata = new
            {
                Alerts.TotalCount,
                Alerts.PageSize,
                Alerts.TotalPages,
                Alerts.CurrentPage,
                Alerts.HasPrevious,
                Alerts.HasNext,
                //  pager.StartDate,
                //   pager.EndDate,
                // pager.Search
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(Alerts);
        }
        [HttpGet("getAbsent")]
        public ActionResult GetAllAbsentEmployee()
        {
            RecurringJob.AddOrUpdate("AlertRecurringJob", () => _alertService.AddAbsentEmployeeAlert(), "0 0 * * MON-FRI");
            return Ok(_alertService.AddAbsentEmployeeAlert());
        }
        [HttpGet("GetAlerts")]
        public ActionResult ShowAlerts()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var ClaimEmployeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = _alertService.AddAbsentEmployeeAlert();
            return Ok(response);
        }
        [HttpPost("GetAlertsByEmployeeId/{id}")]
        public IActionResult GetAlertsByEmployeeId(int id)
        {
            var alerts = _alertService.GetAlertsByEmployeeId(id);
            return Ok(alerts);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlert([FromBody] List<DateTime> attendenceDates, [FromRoute] int id)
        {
            foreach (var attendenceDate in attendenceDates)
            {
                _alertService.DeleteAlertByEmployeeId(id, attendenceDate);
            }
            
            return Ok();
        }
        [HttpGet("GetAlertById/{id}")]
        public async Task<ActionResult<AlertDto>> GetAlertById(int id)
        {
            var alert = await Task.FromResult(_alertService.GetAlertById(id));
            return Ok(alert);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAlert(AlertDto alertDto)
        {
            _alertService.UpdateAlert(alertDto);
            return Ok();
        }

        [HttpGet("GetAlertsHavingEmployeeId")]
        public IActionResult GetAlertsHavingEmployeeId()
        {
            var alerts = _alertService.GetAlertsHavingEmployeeId();
            return Ok(alerts);
        }

        [HttpGet("GetAlertByAttendenceDateAndEmployeeId")]
        public async Task<IActionResult> GetAlertByAttendenceDateAndEmployeeId([FromQuery] string attendenceDate, [FromQuery] int employeeId)
        {
            var alertDto =await  _alertService.GetAlertByAttendenceDateAndEmployeeId(DateTime.Parse(attendenceDate),employeeId);
            return Ok(alertDto);
        }
    }
}
