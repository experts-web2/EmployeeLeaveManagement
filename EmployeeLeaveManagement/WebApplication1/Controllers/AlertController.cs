using DAL.Interface;
using ELM.Helper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost]
        public ActionResult GetAlert(Pager pager)
        {
            try
            {
                var Alerts = _alertRepository.GetAllAlert(pager);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAbsent")]
        public ActionResult GetAllAbsentEmployee()
        {
              RecurringJob.AddOrUpdate("AlertRecurringJob", () => _alertRepository.AddAbsentEmployeeAlert(), "0 0 0 * * Mon-Fri");
            return Ok(_alertRepository.AddAbsentEmployeeAlert());
        }
        
    }
}
