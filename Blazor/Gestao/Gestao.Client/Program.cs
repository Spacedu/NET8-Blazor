using Blazored.LocalStorage;
using Gestao.Client;
using Gestao.Client.Libraries.Notifications;
using Gestao.Client.Services;
using Gestao.Domain.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IConfiguration>(options =>
{
    return builder.Configuration;
});
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


builder.Services.AddScoped<HttpClient>(options => {
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("http://localhost:5262");

    return httpClient;
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CompanyOnSelectedNotification>();

builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionService>();

await builder.Build().RunAsync();
