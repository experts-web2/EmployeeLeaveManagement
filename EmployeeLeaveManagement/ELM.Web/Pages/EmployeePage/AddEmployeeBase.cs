using DTOs;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.EmployeePage
{
    public class AddEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public EmployeeDto EmployeeDto { get; set; } = new();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public int? ID { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            if (ID.HasValue)
            {
                
                EmployeeDto = await EmployeeService.GetEmployeebyId(ID.Value);
            }
        }

        protected async Task SaveEmployee()
        {
            if (ID != 0)
            {
                await EmployeeService.UpdateEmployee(EmployeeDto);
            }
            else
            {
                await EmployeeService.AddEmployee(EmployeeDto);
            }
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("ListofEmployee");
        }
    }
}
