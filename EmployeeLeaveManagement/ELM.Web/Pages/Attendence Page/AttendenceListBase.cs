using DTOs;
using ELM.Helper;
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
        public AttendenceDto SelectedAttendence { get; set; } =new();
        public Pager Paging { get; set; } = new();
        public int Role { get; set; } = 1;

        public void SetAttendenceId(int id)
        {
            SelectedAttendence = AttendenceDtoList.FirstOrDefault(x => x.ID == id);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        public async Task GetAll(int currentPage=1)
        {
            Paging.CurrentPage = currentPage;
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
