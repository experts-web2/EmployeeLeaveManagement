﻿using BL.Interface;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public IActionResult AddLoan(LoanDto loanDto)
        {
            if (loanDto.ID>0)
            {
                _loanService.Update(loanDto);
                return Ok(loanDto);
            }
            else
            {
                _loanService.AddLoan(loanDto);
                return Ok(loanDto);
            }
           
        }


        [HttpGet]
        public IActionResult GetAllLoans()
        {
            var allLoanList = _loanService.GetAllLoans();
            return Ok(allLoanList);
        }

        [HttpGet("GetLoanByEmployeeId")]
        public IActionResult GetLoanByEmployeeId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var ClaimEmployeeId = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(ClaimEmployeeId, out int employeeID)) 
            {
                var allLoanList = _loanService.GetLoanByEmployeeId(employeeID);
                return Ok(allLoanList);
            }
            return Ok(null);
            
        }
    }
}
