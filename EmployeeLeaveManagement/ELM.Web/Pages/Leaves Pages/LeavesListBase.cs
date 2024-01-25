
using DAL.Interface;
using DomainEntity.Models;
using DTOs;
using ELM.Helper;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace EmpLeave.Web.Pages.Leaves_Pages
{
    public class LeavesListBase : ComponentBase
    {
        [Inject]
        public ILeaveService LeaveService { get; set; }
        [Inject]
        public ILeaveHistoryService LeaveHistoryService { get; set; }
        public List<LeaveHistory> leaveHistories { get; set; } = new();
        public List<LeaveDto> LeaveDtosList { get; set; } = new();
        public LeaveDto SelectedLeave { get; set; } = new LeaveDto();
        public Pager Paging { get; set; } = new();

        public void SetLeaveID(int id)
        {
            SelectedLeave = LeaveDtosList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {

            leaveHistories = await LeaveHistoryService.GetLeaveHistoryByEmployeeId();
            await GetAll();
        }
        public async Task GetAll(int currentPage = 1)
        {
            Paging.CurrentPage = currentPage;
            var LeaveDto = await LeaveService.GetAllLeaves(Paging);
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
            await LeaveService.DeleteLeave(id);
            await GetAll();

        }
    }
}
