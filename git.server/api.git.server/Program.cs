using api.git.server.Interfaces;
using api.git.server.Operations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IGitRepositoryOperation, GitRepositoryOperation>();
builder.Services.AddScoped<IAccountOperation, AccountOperation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
