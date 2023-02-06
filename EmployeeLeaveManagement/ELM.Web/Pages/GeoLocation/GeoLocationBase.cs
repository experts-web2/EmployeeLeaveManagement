using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ELM.Web.Pages.GeoLocation
{
    public class GeoLocationBase:ComponentBase
    {
        public readonly Lazy<Task<IJSObjectReference>> moduleTask = default!;
        public GeoCoordinates? geoCoordinates = null;

        [Inject]
        public IJSRuntime jsRuntime { get; set; } = default!;


        public async Task GetLocationAsync()
        {
            await jsRuntime.InvokeVoidAsync(identifier: "geoFindMe");
        }

        [JSInvokable]
        public async Task OnSuccessAsync(GeoCoordinates geoCoordinates)
        {
            this.geoCoordinates = geoCoordinates;
            await InvokeAsync(StateHasChanged);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        public class GeoCoordinates
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Accuracy { get; set; }
        }
    }
}
