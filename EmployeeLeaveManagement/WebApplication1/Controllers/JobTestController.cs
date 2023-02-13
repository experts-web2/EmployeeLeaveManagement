﻿using DAL.Interface;
using Hangfire;
using Microsoft.AspNetCore.Mvc;


namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTestController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobTestController(IJobService jobService, IBackgroundJobClient backgroundJobClient)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpGet]
        public ActionResult GetAttendanceJob()
        {
            var AllAttendnce = _backgroundJobClient.Enqueue(() => _jobService.GetAllAttendences());
            return Ok(AllAttendnce);
        }

        [HttpGet("GetAllAbsentEmployee")]
        public ActionResult GetAllAbsentEmployee()
        {
            
            RecurringJob.AddOrUpdate("myrecurringjob", ()=> _jobService.GetAbsentEmployee(),
                                  Cron.Daily);
            return Ok(_jobService.GetAbsentEmployee());
        }

    }

}
