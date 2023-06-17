using BreakingNomad.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BreakingNomad.Ui;
using BreakingNomad.Ui.Components.MenuMaker;
using BreakingNomad.Ui.Pages;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IMenuLookup, MenuLookup>();
builder.Services.AddScoped<GreeterClient>();
builder.Services.AddScoped<IGreeterService>(services =>
{
  Console.Out.WriteLine("Connect");
  var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
  var channel = GrpcChannel.ForAddress("http://localhost:5200", new GrpcChannelOptions { HttpClient = httpClient });
  return channel.CreateGrpcService<IGreeterService>();
});


await builder.Build().RunAsync();

