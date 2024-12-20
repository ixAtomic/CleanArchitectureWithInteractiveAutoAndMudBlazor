using Application;
using Infrastructure;
using Microsoft.Data.SqlClient;
using Presentation.Server.Components;
using System.Data;
using Domain;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services
    .AddInfrastructure()
    .AddApplication();

builder.Services.AddMudServices();

var ConnectionStringConfig = builder.Configuration.GetRequiredSection("ConnectionStrings");
var ConnectionStrings = ConnectionStringConfig.Get<ConnectionStrings>() ?? throw new Exception("Section must exist");

builder.Services.AddSingleton<IDictionary<string, IDbConnection>>((x) =>
{
    return new Dictionary<string, IDbConnection>()
    {
        { nameof(ConnectionStrings.WeatherDb), new SqlConnection(ConnectionStrings.WeatherDb) },
        { nameof(ConnectionStrings.OtherDb), new SqlConnection(ConnectionStrings.OtherDb) }
    };
});

builder.Services.Configure<ConnectionStrings>(ConnectionStringConfig);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Presentation.Client._Imports).Assembly);

app.Run();
