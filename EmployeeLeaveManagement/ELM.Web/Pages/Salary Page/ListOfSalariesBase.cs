using DTOs;
using ELM.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.Salary_Page
{
    public class ListOfSalariesBase :ComponentBase
    {

        [Inject]
        public ISalaryHistory SalaryService { get; set; }

        public List<SalaryHistoryDto> SalaryDtoList { get; set; } = new();
        public SalaryHistoryDto SelectedSalary { get; set; }=new();

        public void SetSalaryId(int id)
        {
            SelectedSalary = SalaryDtoList.FirstOrDefault(x => x.ID == id);
        }
        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public async Task GetAll()
        {
           SalaryDtoList = await SalaryService.GetSalaries();
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
