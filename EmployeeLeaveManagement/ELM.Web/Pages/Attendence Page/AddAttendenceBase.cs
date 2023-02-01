﻿using DTOs;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

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
       
        [Parameter]
        public int? ID { get; set; }
        public List<EmployeeDto> EmployeeDtosList { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            EmployeeDtosList = await EmployeeService.GetAllEmployee();
        }
        protected async Task SaveAttendence()
        {
            if (!ID.HasValue)
            {
                await AttendenceService.PostCall(AttendenceDto);
            }
            else
            {
                Cancel();
            }
       
        }

        public void Cancel()
        {
            NavigationManager.NavigateTo("/ListofAttendence");
        }
    }
}

