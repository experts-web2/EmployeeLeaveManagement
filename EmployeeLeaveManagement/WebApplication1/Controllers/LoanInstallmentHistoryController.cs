using BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpLeave.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanInstallmentHistoryController : ControllerBase
    {
        private readonly ILoanInstallmentHistoryService _loanInstallmentHistoryService;

        public LoanInstallmentHistoryController(ILoanInstallmentHistoryService loanInstallmentHistoryService)
        {
            _loanInstallmentHistoryService = loanInstallmentHistoryService;
        }

        [HttpGet]
        public IActionResult GetAllLoanInstallmentHistory()
        {
           var listOfLoansInstallments = _loanInstallmentHistoryService.GetAllLoanHistory();
            return Ok(listOfLoansInstallments);
        }


        [HttpGet("GetLoanInstallmentById/{employeeId}")]
        public IActionResult GetAllLoanInstallmentHistoryOfEmployee(int employeeId)
        {
            var listOfLoansInstallmentsByID = _loanInstallmentHistoryService.GetAllLoanHistoryofEmployee(employeeId);
            return Ok(listOfLoansInstallmentsByID);
        }
    }
}
