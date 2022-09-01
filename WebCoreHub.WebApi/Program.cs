using Microsoft.EntityFrameworkCore;
using WebCoreHub.Dal;
using WebCoreHub.Models;
using WebCoreHub.WebApi.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<WebCoreHubDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConStr")));
builder.Services.AddTransient<ICommonRepository<Employee>, CommonRepository<Employee>>();
builder.Services.AddTransient<ICommonRepository<Event>, CommonRepository<Event>>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
// AddScoped()
// This methods creates a Scoped service. A new instance of a Scoped service created one per request within the scope.
// For Example, in a web application creates 1 instance per each http request but uses the same instance in the other calls that same web request.
builder.Services.AddScoped<ITokenManager, TokenManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
