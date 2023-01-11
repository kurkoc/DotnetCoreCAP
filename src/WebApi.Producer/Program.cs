using Contracts;
using DotNetCore.CAP;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using WebApi.Producer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string sqlConnString = "Server=localhost;Database=CapTestDB;Trusted_Connection=True;TrustServerCertificate=True";

builder.Services.AddCap(options =>
{
    options.UseSqlServer(configure =>
    {
        configure.ConnectionString = sqlConnString;
    });
    options.UseRabbitMQ(configure =>
    {
        configure.HostName = "localhost";
        configure.Port = 5672;
        configure.UserName = "guest";
        configure.Password = "guest";
    });

    options.UseDashboard();
});

builder.Services.AddScoped<LogConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "it works!");

app.MapGet("/publish", async (ICapPublisher publisher) =>
{
    await publisher.PublishAsync<LogMessage>("queue.cap.log", new LogMessage { Id = Guid.NewGuid(), Message = "sample message", Type = 1 });
});


app.Run();
