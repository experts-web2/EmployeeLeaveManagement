using DomainEntity.Pagination;
using DTOs;
using ELM.Web.Helper;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Pager = ELM.Web.Helper.Pager;

namespace EmpLeave.Web.Pages.Leaves_Pages
{
    public class LeavesListBase : ComponentBase
    {
        [Inject]
        public ILeaveService LeaveService { get; set; }
        public List<LeaveDto> LeaveDtosList { get; set; } = new();
        public LeaveDto SelectedLeave { get; set; } = new LeaveDto();
        public Pager Paging { get; set; } = new();

        public void SetLeaveID(int id)
        {
            SelectedLeave = LeaveDtosList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public Parameter parameter = new();
        public async Task GetAll(int currentPage = 1)
        {
            parameter.page = currentPage;
            var LeaveDto = await LeaveService.GetAllLeaves(parameter);
            LeaveDtosList = LeaveDto.DataList;
            Paging = LeaveDto.Pager;
            StateHasChanged();

        }
        public void DeleteConfirm(int Id)
        {
            SelectedLeave = LeaveDtosList.FirstOrDefault(x => x.ID == Id);
        }

        public async Task DeleteEmployee(int id)
        {
            await LeaveService.DeleteCall(id);
            await GetAll();

        }
    }
}
