﻿using DAL.Interface;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(response);
            }
            else return BadRequest();
        }
        [HttpGet("GetAllAttendences")]
        public IActionResult GetAllAttendences()
        {
            var response = attendenceRepository.GetAllAttendences();
            return Ok(response);
        }
    }
}