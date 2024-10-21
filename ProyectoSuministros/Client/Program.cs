using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProyectoSuministros.Client;
using ProyectoSuministros.Client.Repositorios;
using Radzen;
using ProyectoSuministros.Client.Helpers.Validations;
using ProyectoSuministros.Client.Helpers;
using ProyectoSuministros.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient
    {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
    Timeout = TimeSpan.FromMinutes(15),
    });
ConfigureServices(builder.Services);

builder.Logging.SetMinimumLevel(LogLevel.Information);

await builder.Build().RunAsync();
void ConfigureServices(IServiceCollection services)
{
    services.AddSweetAlert2();
    services.AddScoped<IRepositorio, Repositorio>();

    services.AddAuthorizationCore();

    services.AddScoped<ProveedorAutenticacionJWT>();

    services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(proveedor =>
    proveedor.GetRequiredService<ProveedorAutenticacionJWT>());

    services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(proveedor =>
    proveedor.GetRequiredService<ProveedorAutenticacionJWT>());

    services.AddScoped<RenovadorToken>();
    services.AddSingleton(new CultureInfo("es-MX"));
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
    services.AddScoped<GroupClientValidation>();
    services.AddScoped<UsuarioInfoValidation>();

    services.AddScoped<ClientDestinationValidation>();
    services.AddScoped<ClusterDestinationValidation>();
    services.AddScoped<EjecutiveDestinationValidation>();
    services.AddScoped<FranchiseDestinationValidation>();
    services.AddScoped<DistributionDestinationValidation>();
    services.AddScoped<TadDestinationValidation>();
    services.AddScoped<ZoneDestinationValidation>();

    services.AddScoped<Constructor_De_URL_Parametros>();
}