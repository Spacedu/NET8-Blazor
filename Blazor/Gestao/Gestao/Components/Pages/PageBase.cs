using Blazored.LocalStorage;
using Gestao.Data;
using Microsoft.AspNetCore.Components;

namespace Gestao.Components.Pages
{
    public class PageBase : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorage { get; set; } = null!;
        [Inject] public ApplicationDbContext DB { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    }
}
