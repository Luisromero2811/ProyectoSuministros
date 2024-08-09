using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProyectoSuministros.Client;
using ProyectoSuministros.Client.Repositorios;
using Radzen;
using ProyectoSuministros.Client.Helpers.Validations;
using ProyectoSuministros.Client.Helpers;

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
    services.AddSweetAlert2();
    services.AddScoped<IRepositorio, Repositorio>();

    services.AddScoped<GestionDestinoValidation>();
    services.AddScoped<GestionClienteValidation>();
    services.AddScoped<GestionClusterValidation>();
    services.AddScoped<GestionEjecutivosValidation>();
    services.AddScoped<GestionFranquiciasValidation>();
    services.AddScoped<GestionGrupoValidation>();
    services.AddScoped<GestionRazonSocialValidation>();
    services.AddScoped<GestionRepartoValidation>();
    services.AddScoped<GestionTADValidation>();
    services.AddScoped<GestionZonaValidation>();

    services.AddScoped<Constructor_De_URL_Parametros>();
}