﻿using BL.Interface;
using DAL.Interface;
using DTOs;
using ELM.Helper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, IJobService jobService, IBackgroundJobClient backgroundJobClient,IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
            _employeeService = employeeService;

        }
        [HttpPost("getall")]
        public IActionResult GetAllEmployee(Pager pager)
        {
            try
            {
                var allemployees = _employeeRepository.GetAllEmployee(pager);
                var metadata = new
                {
                    allemployees.TotalCount,
                    allemployees.PageSize,
                    allemployees.TotalPages,
                    allemployees.CurrentPage,
                    allemployees.HasPrevious,
                    allemployees.HasNext,
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(allemployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AddEmployee(EmployeeDto employeeDto)
        {
            _employeeService.AddEmployee(employeeDto);
            return Ok("Added Succesfully");
        }

        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeDto employee)
        {
            _employeeRepository.Update(employee);
            return Ok("Updated Succesfully");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return Ok("Deleted Succesfully");
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
            var employeeDto = _employeeRepository.GetById(id);
            return Ok(employeeDto);
        }
        [HttpGet("GetAllEmployees")]
        public IActionResult GetAll()
        {
           var ListOfEmployees= _employeeRepository.GetAllEmployees();
            return Ok(ListOfEmployees);
        }
        [HttpGet("GetAllAbsentEmployee")]
        public ActionResult GetAllAbsentEmployee()
        {
            RecurringJob.AddOrUpdate("myrecurringjob", () => _jobService.GetAbsentEmployee(), "0 0 * * MON-FRI");
            return Ok(_jobService.GetAbsentEmployee());
        }
        [HttpGet("GetAllAttendance")]
        public ActionResult GetAttendanceJob()
        {
            var AllAttendnce = _backgroundJobClient.Enqueue(() => _jobService.GetAllAttendences());
            return Ok(AllAttendnce);
        }

    }
}
