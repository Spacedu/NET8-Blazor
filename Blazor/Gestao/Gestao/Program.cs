using Blazored.LocalStorage;
using Coravel;
using Gestao.Client.Libraries.Notifications;
using Gestao.Client.Pages;
using Gestao.Components;
using Gestao.Components.Account;
using Gestao.Data;
using Gestao.Data.Repositories;
using Gestao.Domain.Enums;
using Gestao.Domain.Repositories;
using Gestao.Libraries.Mail;
using Gestao.Libraries.Queues;
using Gestao.Libraries.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

#region Config of Authentication
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration.GetValue<String>("OAuth:Google:ClientId")!;
        options.ClientSecret = builder.Configuration.GetValue<String>("OAuth:Google:ClientSecret")!;
    })
    .AddFacebook(options =>
    {
        options.ClientId = builder.Configuration.GetValue<String>("OAuth:Facebook:ClientId")!;
        options.ClientSecret = builder.Configuration.GetValue<String>("OAuth:Facebook:ClientSecret")!;
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = builder.Configuration.GetValue<String>("OAuth:Microsoft:ClientId")!;
        options.ClientSecret = builder.Configuration.GetValue<String>("OAuth:Microsoft:ClientSecret")!;
    })
    .AddIdentityCookies();
#endregion

#region Config of Database & Authentication
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
#endregion

#region Dependecy Injection (Email, Repositories, Extra Library: LocalStorage, Queuing)
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddQueue();

builder.Services.AddScoped<FinancialTransactionRepeatInvocable>();

builder.Services.AddSingleton<SmtpClient>(options =>
{
    var smtp = new SmtpClient();
    smtp.Host = builder.Configuration.GetValue<string>("EmailSender:Server")!;
    smtp.Port = builder.Configuration.GetValue<int>("EmailSender:Port");
    smtp.EnableSsl = builder.Configuration.GetValue<bool>("EmailSender:SSL");

    smtp.Credentials = new NetworkCredential(
        builder.Configuration.GetValue<string>("EmailSender:User"),
        builder.Configuration.GetValue<string>("EmailSender:Password")
    );

    return smtp;
});
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();
builder.Services.AddSingleton<ICepService, CepService>();
builder.Services.AddScoped<CompanyOnSelectedNotification>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//Módulo de Blazor Server/WASM sendo Habilitado
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Gestao.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

#region Minimal APIs
int pageSize = builder.Configuration.GetValue<int>("Pagination:PageSize");

app.MapGet("/api/categories", async (
    ICategoryRepository repository,
    [FromQuery] int companyId,
    [FromQuery] int pageIndex) =>
{
    var data = await repository.GetAll(companyId, pageIndex, pageSize);

    return Results.Ok(data);
});

app.MapGet("/api/companies", async (
    ICompanyRepository repository,
    [FromQuery]Guid applicationUserId,
    [FromQuery] int pageIndex,
    [FromQuery] string searchWord
) => {

    var data = await repository.GetAll(applicationUserId, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});

app.MapGet("/api/accounts", async(
    IAccountRepository repository,
    [FromQuery] int companyId,
    [FromQuery] int pageIndex,
    [FromQuery] string searchWord
) =>{
    var data = await repository.GetAll(companyId, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});

app.MapGet("/api/financialtransactions", async(
    IFinancialTransactionRepository repository,
    [FromQuery] TypeFinancialTransaction type,
    [FromQuery] int companyId,
    [FromQuery] int pageIndex,
    [FromQuery] string searchWord
) =>{
    var data = await repository.GetAll(companyId, type, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});

#endregion
app.Run();