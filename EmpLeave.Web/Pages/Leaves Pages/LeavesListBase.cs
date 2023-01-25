using DTOs;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.Leaves_Pages
{
    public class LeavesListBase : ComponentBase
    {
        [Inject]
        public ILeaveService LeaveService { get; set; }
        public List<LeaveDto> LeaveDtosList { get; set; } = new();
        public LeaveDto SelectedLeave { get; set; } = new LeaveDto();
       
        public void SetLeaveID(int id)
        {
            SelectedLeave = LeaveDtosList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public async Task GetAll()
        {
            LeaveDtosList = await LeaveService.GetAllLeaves();
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
