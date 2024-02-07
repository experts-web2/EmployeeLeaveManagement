using DTOs;
using ELM_DAL.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ELM.Web.Pages.DailTimeSheet
{
    public class DailyTimeSheetBase :  ComponentBase
    {
        [Inject]
        public IDailyTimeSheetService dailyTimeSheetService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public DailyTimeSheetDto DailyTimeSheetDto { get; set; } = new();
        public List<DailyTimeSheetDto> dailyTimeSheetDtos { get; set; } = new List<DailyTimeSheetDto>();

        protected override async Task OnInitializedAsync()
        {
            dailyTimeSheetDtos = await dailyTimeSheetService.GetAllDailyTimeSheet();
        }

        public async Task<string> AddDailyTimeSheet()
        {
            var response = await dailyTimeSheetService.AddDailyTimeSheet(DailyTimeSheetDto);
            NavigationManager.NavigateTo("/DailyTask");
            return response;
        }
        public async Task ShowAlert(string message)
        {
            await JSRuntime.InvokeVoidAsync("alert", message);
        }
    }
}
