using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web_ITSC_Client;
using Web_ITSC_Client.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Registra los componentes ra�z de la aplicaci�n Blazor
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registro de servicios
// Configuraci�n para las llamadas HTTP (por ejemplo, si vas a hacer llamadas al servidor)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Registro del servicio HttpServicios para interactuar con el backend (API)
builder.Services.AddScoped<IHttpServicios, HttpServicios>();

// Aqu� registramos el servicio de roles que se utilizar� en el cliente para manejar los roles del usuario
builder.Services.AddSingleton<ServicioRol>();

// Si tienes alg�n otro servicio espec�fico del cliente, puedes agregarlo aqu�
// builder.Services.AddScoped<OtroServicio>();

await builder.Build().RunAsync();