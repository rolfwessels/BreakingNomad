using System.IO.Compression;
using System.Reflection;
using BreakingNomad.Api.Data;
using BreakingNomad.Shared;
using ProtoBuf.Grpc.Server;


const string corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(_=> new DataStoreFactory(@"D:\Work\Home\BreakingNomad\var\sampledata"));
builder.Services.AddScoped(provider=> new MenuService(provider.GetRequiredService<DataStoreFactory>().PlannedTrips));

builder.Services.AddCors(options =>
{
  options.AddPolicy(corsPolicy,
    p =>
    {
      p.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:5201");
    });
});
builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });


var app = builder.Build();
app.UseCors(corsPolicy);

app.UseRouting();
app.UseGrpcWeb();

app.UseEndpoints(endpoints =>
{
  endpoints.MapGrpcService<GreeterService>();
});


app.MapGet("/", () => $"Welcome to Breaking nomad {Assembly.GetEntryAssembly()!.GetName().Version}");

app.Run();

