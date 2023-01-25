using ELM.Shared;
using EmpLeave.Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace EmpLeave.Web.Pages.AuthPage.LogCredential
{
    public class SigninBase :ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public LogIn LogIn { get; set; } = new();
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject]
        IJSRuntime? JsRuntime { get; set; }

        //private bool ShowErrors;
        //private string Error = "";
        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;
            if (user.Identity.IsAuthenticated) await JsRuntime.InvokeVoidAsync("history.back");
        }

        public void SignIn()
        {
          var response=  RegisterService.SignInCall(LogIn);
            if (response.IsCompleted)
            {
                NavigationManager.NavigateTo("addemployee");
            }
            NavigationManager.NavigateTo("login");
        
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/Employeelist");
        }
    }
}
