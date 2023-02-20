using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM.Web.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.Salary_Page
{
    public class ListOfSalariesBase :ComponentBase
    {

        [Inject]
        public ISalaryHistory SalaryService { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public List<SalaryHistoryDto> SalaryDtoList { get; set; } = new();
        public SalaryHistoryDto SelectedSalary { get; set; }=new();
        public List<Employee> EmployeesList { get; set; } = new();
        public DateTime? StartDate { get; set; }=DateTime.Now.Date;
        public DateTime EndDate { get; set; }=DateTime.Now.Date;
        public string Search { get; set; } = string.Empty;
        public Pager Paging { get; set; } = new();

        public void SetSalaryId(int id)
        {
            SelectedSalary = SalaryDtoList.FirstOrDefault(x => x.ID == id);
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
           var SalaryDto = await SalaryService.GetSalaries(Paging);
            SalaryDtoList = SalaryDto.DataList;
            Paging = SalaryDto.Pager;
            StateHasChanged();
        }
        public void DeleteConfirm(int Id)
        {
            SelectedSalary = SalaryDtoList.FirstOrDefault(x => x.ID == Id);
        }
        public async Task DeleteSalary(int id)
        {
            await SalaryService.DeleteSalary(id);
            await GetAll();
        }
    }
}
