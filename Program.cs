using Microsoft.EntityFrameworkCore;
using peliculas_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Se agregar swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
opciones.UseSqlServer("name=DefaultConnection"));
//Se agrega el servicio de AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Se Agrega cache 1
builder.Services.AddOutputCache(opciones =>
    {
        opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);

    });

//Se agregan permisos por url
var originesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");

//Se agregan permisos de CORS
// Permite el acceso a la API desde cualquier origen
builder.Services.AddCors(
    opciones =>
    {
        opciones.AddDefaultPolicy(
            opcionesCORS =>
            {   //Se agrega la variable que contiene los orígenes permitidos
                opcionesCORS.WithOrigins(originesPermitidos).AllowAnyMethod().AllowAnyHeader();
            });
    }

    );

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();
//Se agrega cache 1.1
app.UseOutputCache();

app.UseAuthorization();
app.MapControllers();

app.Run();
