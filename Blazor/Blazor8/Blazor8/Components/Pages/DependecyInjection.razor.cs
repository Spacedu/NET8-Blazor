using Blazor8.Models;
using Microsoft.AspNetCore.Components;

namespace Blazor8.Components.Pages
{
    public partial class DependecyInjection
    {
        [Inject(Key = "sms")]
        public IMensagem MensagemChaveada { get; set; } = default!;
    }
}
