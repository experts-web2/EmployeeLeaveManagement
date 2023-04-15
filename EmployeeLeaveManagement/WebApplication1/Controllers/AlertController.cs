﻿using BL.Interface;
using DAL.Interface;
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
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var Role = identity?.FindFirst(ClaimTypes.Role);
                var ClaimRoleId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Role.Value.Contains("Admin"))
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
                else
                    return GetAlertsByEmployeeId(int.Parse(ClaimRoleId));
            
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
            var response= _alertService.AddAbsentEmployeeAlert();
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAlertsByEmployeeId(int id)
        {
            var alerts = _alertService.GetAlertsByEmployeeId(id);
            return Ok(alerts);
        }

    }
}
