using GrpcGreeter.Models;
using GrpcGreeter.Services;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
//BsonClassMap.RegisterClassMap<Human>();
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.Configure<WorkersListDatabaseSettings>(
    builder.Configuration.GetSection("WorkersListDatabase"));
builder.Services.AddSingleton<WorkersService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
/* Web
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/ //Web
app.Run();
