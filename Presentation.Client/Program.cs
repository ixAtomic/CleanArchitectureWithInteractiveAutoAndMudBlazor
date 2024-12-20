using Application.Public.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Presentation.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

var API = builder.Configuration.GetConnectionString("API") ?? throw new Exception("Ya need the route to your apis here..");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(API) });

builder.Services.AddSingleton<IWeatherService, PublicWeatherService>();

await builder.Build().RunAsync();
