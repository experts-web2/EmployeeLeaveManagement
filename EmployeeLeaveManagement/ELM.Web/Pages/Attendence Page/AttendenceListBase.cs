using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace ELM.Web.Pages.Attendence_Page
{
    public class AttendenceListBase:ComponentBase
    {

        [Inject]
        public IAttendenceService AttendenceService { get; set; }
        [Inject]

        public IEmployeeService EmployeeService { get; set; }
        public List<AttendenceDto> AttendenceDtoList { get; set; } = new();
        public AttendenceDto SelectedAttendence { get; set; } =new();
        public List<Employee> EmployeesList { get; set; } = new();
        public DateTime? StartDate { get; set; } = DateTime.Now.Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
        public string Search { get; set; }= string.Empty;
        public Pager Paging { get; set; } = new();

        public void SetAttendenceId(int id)
        {
            SelectedAttendence = AttendenceDtoList.FirstOrDefault(x => x.ID == id);
        }
        protected override async Task OnInitializedAsync()
        {
            EmployeesList = await EmployeeService.GetAllEmployee();
            await GetAll();
        }
        public async Task GetAll(int currentPage=1)
        {
            Paging.CurrentPage = currentPage;
            Paging.StartDate = StartDate;
            Paging.EndDate = EndDate;
            Paging.Search = Search;
            var attendenceDto = await AttendenceService.GetAttendences(Paging);
            AttendenceDtoList = attendenceDto.DataList;
            Paging = attendenceDto.Pager;
            StateHasChanged();
        }
        public void DeleteConfirm(int Id)
        {
            SelectedAttendence = AttendenceDtoList.FirstOrDefault(x => x.ID == Id);
        }
        public async Task DeleteAttendence(int id)
        {
            await AttendenceService.DeleteAttendence(id);
            await GetAll();

        }
    }
}
