using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProyectoSuministros.Client;
using ProyectoSuministros.Client.Repositorios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient
    {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });
ConfigureServices(builder.Services);


await builder.Build().RunAsync();
void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IRepositorio, Repositorio>();
}