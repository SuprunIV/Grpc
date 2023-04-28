using ProviderService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();
app.MapGrpcService<ProviderApiService>();
app.MapGet("/",
    () => "Hello");
       
app.Run();