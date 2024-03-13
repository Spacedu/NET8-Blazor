using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor8.Components.Pages
{
    public partial class Separation
    {
        /*
        * Razor (Engine Template - ASP.NET): HTML/CSS/JS + C#
        * 
        * Blazor: .razor (componentes blazor)
        * ASP.NET .cshtml (MVC & Razor Pages)
        */
        
        public string Texto = "Oi eu sou código CSharp!";

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        public async Task CarregarJS()
        {
            var modulo = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Pages/Separation.razor.js");

            //await JSRuntime.InvokeVoidAsync("ShowMessage");

            await modulo.InvokeVoidAsync("ShowMessageTwo");
        }
    }
}
