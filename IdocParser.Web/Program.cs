using IdocParser.Business.Interfaces;
using IdocParser.Business.Services;
using IdocParser.Persistence.Interfaces;
using IdocParser.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure IDocs storage directory
string idocsStorageDir = Path.Combine(builder.Environment.ContentRootPath, "IdocsStorage");

// Register services
builder.Services.AddSingleton<IIdocRepository>(provider => 
    new FileIdocRepository(
        idocsStorageDir, 
        provider.GetRequiredService<Microsoft.Extensions.Logging.ILogger<FileIdocRepository>>()));

builder.Services.AddScoped<IIdocGeneratorService, IdocGeneratorService>();

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
