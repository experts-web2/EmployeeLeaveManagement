using DTOs;
using ELM.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace ELM.Web.Pages.Attendence_Page
{
    public class AttendenceListBase:ComponentBase
    {

        [Inject]
        public IAttendenceService AttendenceService { get; set; }

        public List<AttendenceDto> AttendenceDtoList { get; set; } = new();
        public AttendenceDto SelectedAttendence { get; set; } = new();
        public void SetAttendenceId(int id)
        {
            SelectedAttendence = AttendenceDtoList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public async Task GetAll()
        {
            AttendenceDtoList = await AttendenceService.GetAttendences();
        }
        public void DeleteConfirm(int Id)
        {
            SelectedAttendence = AttendenceDtoList.FirstOrDefault(x => x.ID == Id);
        }

        public async Task DeleteEmployee(int id)
        {
            await AttendenceService.DeleteCall(id);
            await GetAll();

        }
    }
}
