﻿using BL.Interface;
using DomainEntity.Pagination;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost("getall")]
        public IActionResult GelAllLeaves(Pager pager)
        {
            try
            {
                var AllLeave = _leaveService.GetAll(pager);
                var metadata = new
                {
                    AllLeave.TotalCount,
                    AllLeave.PageSize,
                    AllLeave.TotalPages,
                    AllLeave.CurrentPage,
                    AllLeave.HasPrevious,
                    AllLeave.HasNext,
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(AllLeave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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