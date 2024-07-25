using CustomerService.Extensions;
using Hangfire;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.ConfigureSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
