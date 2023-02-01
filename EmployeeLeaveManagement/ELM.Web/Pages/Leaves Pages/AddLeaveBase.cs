using DomainEntity.Models;
using DTOs;
using ELM.Helper.SupportFiles;
using ELM.Web.Helper;
using EmpLeave.Web.Services.Interface;
using EmpLeave.Web.Services.ServiceRepo;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.Leaves_Pages
{
    public class AddLeaveBase : ComponentBase
    {
        [Inject]
        public ILeaveService LeaveService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
       // public Parameter parameter { get; set; }
        public Pager Paging { get; set; }
        public List<EmployeeDto> EmployeeDtosList { get; set; } = new();
       
        public LeaveDto LeaveDto { get; set; } = new();
        [Parameter]
        public int? ID { get; set; }
        protected override async Task OnInitializedAsync()
        {
            GetEmployees();
        }
        protected override async Task OnParametersSetAsync()
        {
            if (ID.HasValue)
            {

                LeaveDto = await LeaveService.GetByIdCall(ID.Value);
            }
        }

        protected async Task SaveLeave()
        {
            if (ID.HasValue)
            {
                await LeaveService.UpdateCall(LeaveDto);
            }
            else
            {
                await LeaveService.PostCall(LeaveDto);
            }
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("Listofleaves");
        }
        public async Task GetEmployees()
        {
            Response<EmployeeDto> listEmployee = await  EmployeeService.GetAllEmployee(Paging);
            EmployeeDtosList = listEmployee.DataList;
        }
    }
}
