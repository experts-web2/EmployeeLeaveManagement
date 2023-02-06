using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace EmpLeave.Web.Pages.AuthPage.LogCredential
{
    public class SigninBase :ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public LogIn LogIn { get; set; } = new(); 
        //[CascadingParameter]
        //Task<AuthenticationState> authenticationStateTask { get; set; }
        //[Inject]
        //IJSRuntime? JsRuntime { get; set; }
        //private bool ShowErrors;
        //private string Error = "";
        //protected override async Task OnInitializedAsync()
        //{
        //    var user = (await authenticationStateTask).User;
        //    if (user.Identity.IsAuthenticated) await JsRuntime.InvokeVoidAsync("history.back");
        //}

        public async void SignIn()
        {
            var response=await  RegisterService.SignIn(LogIn);
            if (response)
            {
                NavigationManager.NavigateTo("/addemployee");
            }
            else
            {
                NavigationManager.NavigateTo("/login",true);
            }
        
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/Employeelist");
        }
    }
}
