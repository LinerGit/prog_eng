using Backend.Domain.Interfaces;
using Backend.Domain.Services;
using Backend.Infrastructure.Persistence;
using Backend.Infrastructure.Persistence.Context;
using Backend.Infrastructure.Persistence.Repositories;
using Backend.Presentation.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BalanceService>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyMethod()                     
              .AllowAnyHeader()                     
              .AllowCredentials();                  
    });
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("Angular");
app.UseAuthorization();
app.MapControllers();

app.Run();
