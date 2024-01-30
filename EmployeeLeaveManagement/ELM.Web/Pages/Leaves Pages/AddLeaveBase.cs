using DTOs;
using ELM.Helper;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

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
        public Pager Paging { get; set; } = new();
        public List<EmployeeDto> EmployeeDtosList { get; set; } = new();
       
        public LeaveDto LeaveDto { get; set; } = new();
        [Parameter]
        public int? ID { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await GetEmployees();
            if (ID.HasValue)
            {
                LeaveDto = await LeaveService.GetByIdCall(ID.Value);
            }
        }

        protected async Task SaveLeave()
        {
            await LeaveService.AddLeave(LeaveDto);
            //if (ID.HasValue)
            //{
            //    await LeaveService.EditLeave(LeaveDto);
            //}
            //else
            //{
            //    await LeaveService.AddLeave(LeaveDto);
            //}
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("Listofleaves");
        }
        public async Task GetEmployees()
        {
            Response<EmployeeDto> listEmployee = await  EmployeeService.GetAllEmployeeWithPagination(Paging);
            EmployeeDtosList = listEmployee.DataList;
        }
    }
}
