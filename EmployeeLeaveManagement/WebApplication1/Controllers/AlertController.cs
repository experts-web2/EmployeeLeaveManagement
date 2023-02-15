using DAL.Interface;
using DomainEntity.Models;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        public AlertController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository; 
        }
        [HttpGet]
        public ActionResult GetAlert()
        {
          List<Alert> Alerts= _alertRepository.GetAlerts();
            return Ok(Alerts);
        }
        [HttpGet("getAbsent")]
        public ActionResult GetAllAbsentEmployee()
        {
              RecurringJob.AddOrUpdate("AlertRecurringJob", () => _alertRepository.AddAbsentEmployeeAlert(), "0 0 0 * * Mon-Fri");
            return Ok(_alertRepository.AddAbsentEmployeeAlert());
        }
        
    }
}
