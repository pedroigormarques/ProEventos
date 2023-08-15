using ProEventos.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Application;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IEventoPersist, EventoPersist>();
builder.Services.AddScoped<IGeralPersist, GeralPersist>();
builder.Services.AddScoped<IEventoService, EventoService>();

builder.Services.AddDbContext<ProEventosContext>(
    context => context.UseSqlite(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
