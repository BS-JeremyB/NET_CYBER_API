using Microsoft.EntityFrameworkCore;
using NET_CYBER_API.BLL.Interfaces;
using NET_CYBER_API.BLL.Services;
using NET_CYBER_API.DAL.Data;
using NET_CYBER_API.DAL.Interfaces;
using NET_CYBER_API.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped<ITicketRepository, TicketEntityRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurEntityRepository>();
builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();

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
