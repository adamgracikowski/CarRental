using CarRental.Comparer.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.RegisterConfigurationOptions(builder.Configuration);
builder.Services.RegisterInfrastructureServices();

await builder.Build().RunAsync();
