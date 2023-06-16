using System.Reflection;
using BreakingNomad.Api.Data;
using BreakingNomad.Shared;
using ProtoBuf.Grpc.Server;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(_=> new DataStoreFactory(@"D:\Work\Home\BreakingNomad\var\sampledata"));
builder.Services.AddScoped(provider=> new MenuService(provider.GetRequiredService<DataStoreFactory>().PlannedTrips));
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal; });

// builder.Services.AddCors(setupAction =>
// {
//   setupAction.AddDefaultPolicy(policy =>
//   {
//     policy.AllowAnyHeader()
//       .AllowAnyOrigin()
//       .AllowAnyMethod()
//       .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")
//       .SetPreflightMaxAge(TimeSpan.FromMinutes(20));
//   });
// });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();



var app = builder.Build();
// app.UseCors();
app.UseRouting();
app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

app.UseEndpoints(endpoints =>
{
  endpoints.MapGrpcService<GreeterService>();

  
});


app.MapGet("/", () => $"Welcome to Breaking nomad {Assembly.GetEntryAssembly()!.GetName().Version}");

app.Run();

