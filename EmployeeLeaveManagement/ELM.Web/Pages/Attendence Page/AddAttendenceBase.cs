using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace ELM.Web.Pages.Attendence_Page
{
    public class AddAttendenceBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IAttendenceService AttendenceService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public AttendenceDto AttendenceDto { get; set; } = new();
        public List<Employee> EmployeesList { get; set; } = new();

        [Parameter]
        public int? ID { get; set; }
        [Parameter]
        public bool? isCheckOut { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        public List<EmployeeDto> EmployeeDtosList { get; set; } = new();
        public Pager Paging { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;
            EmployeesList = await EmployeeService.GetAllEmployee();
            var isLogedInUSer = int.TryParse(user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int currentEmployeeID);
            if (!isLogedInUSer) return;


            if (ID.HasValue)
                AttendenceDto = await AttendenceService.GetByID(ID.Value, AttendenceDto.AttendenceDate);
            else
            {
                var attendences = (await AttendenceService.GetAttendences(Paging)).DataList;
                if (attendences.Any())
                {
                    var todayAttendence = attendences.FirstOrDefault(x => x.AttendenceDate.Date == DateTime.Now.Date);
                    if (todayAttendence == null) return;
                    isCheckOut = todayAttendence.Timeout != null; 
                    AttendenceDto = await AttendenceService.GetByID(todayAttendence.ID, DateTime.Now);
                }
            }
        }
        protected async Task SaveAttendence()
        {
            if (!ID.HasValue)
            {
                await AttendenceService.AddAttendence(AttendenceDto);
            }
            else
                await AttendenceService.UpdateAttendence(AttendenceDto);

            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/ListOfAttendence");
        }
    }
}

