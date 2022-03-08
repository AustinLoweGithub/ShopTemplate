using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopTemplate.web.Services.Contracts;
using ShopTemplate.Web;
using ShopTemplate.Web.Services;
using ShopTemplate.Web.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7035/") });

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

await builder.Build().RunAsync();
