using DTOs;
using ELM_DAL.Services.Interface;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ELM.Web.Pages.DailyTask
{
    public class DailyTaskBase : ComponentBase
    {
        [Inject]
        private IDailyTasksService _dailyTasksService {  get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        public bool showModal {  get; set; } = false;
        public List<DailyTaskDto> dailyTaskDtos { get; set; } = new List<DailyTaskDto>();
        public DailyTaskDto dailyTaskDto { get; set; } = new DailyTaskDto();
        protected override async Task OnInitializedAsync()
        {
            var AllTasks = await _dailyTasksService.GetAllDailyTask();
            if (AllTasks.Any())
            {
                dailyTaskDtos = AllTasks;
            }
            else
                dailyTaskDto = new();
        }
        public async Task AddDailyTask()
        {
           await _dailyTasksService.AddDailyTask(dailyTaskDto);
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }
        public void ShowModal()
        {
            showModal = true;
        }
        public void CloseModal()
        {
            showModal = false;
        }
    }
}
