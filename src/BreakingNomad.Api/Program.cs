using System.Reflection;
using BreakingNomad.Api.Data;
using BreakingNomad.Api.Helper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(_=> new DataStoreFactory(@"D:\Work\Home\BreakingNomad\var\sampledata"));
builder.Services.AddScoped(provider=> new MenuService(provider.GetRequiredService<DataStoreFactory>().PlannedTrips));

builder.Services.AddGrpc(options =>
{
  options.EnableDetailedErrors = true;
  options.MaxReceiveMessageSize = 2.Mb();
  options.MaxSendMessageSize = 5.Mb();
});
builder.Services.AddCors(setupAction =>
{
  setupAction.AddDefaultPolicy(policy =>
  {
    policy.AllowAnyHeader()
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
  });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();



var app = builder.Build();
app.UseCors();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseGrpcWeb(new GrpcWebOptions {
  DefaultEnabled = true
});
app.UseEndpoints(endpoints => {
  endpoints.MapGrpcService < MenuService > ();
});
app.MapGet("/", () => $"Welcome to Breaking nomad {Assembly.GetEntryAssembly()!.GetName().Version}");

app.Run();

