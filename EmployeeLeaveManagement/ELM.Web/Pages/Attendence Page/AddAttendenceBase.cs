using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using System;
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
        public int EmployeeId { get; set; }
        public bool isAdmin { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            var user = await authenticationStateTask;
            var u = user.User;
            isAdmin = u.IsInRole("Admin");
            if(isAdmin)
                EmployeesList = await EmployeeService.GetAllEmployee();
            var isUserLogedIn = int.TryParse(u?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int employeeId);
            if (!isUserLogedIn && !ID.HasValue) return;


            if (ID.HasValue)
                AttendenceDto = await AttendenceService.GetByID(ID.Value);
            else
                AttendenceDto = await AttendenceService.GetAttendenceByEmployeeId(employeeId);
            //When Admin loggedin then we use Edit case so we populate EmployeeID from fetched AttendenceDto
            //Reason: If Admin LoggedIn then it use Admin EmployeeID thats why we populate from fetched AttendeceDto
            EmployeeId = AttendenceDto.EmployeeId.HasValue?AttendenceDto.EmployeeId.Value:0;
            SetUserCheckout();
        }
        public async Task HandleChange(int value)
        {
            EmployeeId = value;
            AttendenceDto.EmployeeId = value;
            AttendenceDto = await AttendenceService.GetAttendenceByEmployeeId(value);
            SetUserCheckout();


        }
        private void SetUserCheckout()
        {
            if (!(ID.HasValue) && AttendenceDto.TimeIn?.Date != DateTime.Now.Date)
            {
                isCheckOut = null;
                return;
            }
            if (!AttendenceDto.Timeout.HasValue)
            {
                isCheckOut = false;
                return;
            }
            isCheckOut = AttendenceDto.Timeout != null;
        }
        protected async Task SaveAttendence()
        {
            if (EmployeeId > 0)
                AttendenceDto.EmployeeId = EmployeeId;
            if (AttendenceDto.ID > 0)
            {
                await AttendenceService.UpdateAttendence(AttendenceDto);
            }
            else
                await AttendenceService.AddAttendence(AttendenceDto);
            Cancel();
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/ListOfAttendence");
        }
    }
}

