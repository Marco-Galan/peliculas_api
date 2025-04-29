using peliculas_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Se agregar swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Se Agrega cache 1
builder.Services.AddOutputCache(opciones =>
    {
        opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(10);

    });
//Se Agrega inyeccion de depencia Repositorio en Memoria
//AddTrasient permite configurar servicio
builder.Services.AddTransient<IRepositorio, RepositorioSQLServer>();

//Agregando servicios de memoria
builder.Services.AddTransient<ServicioTrasient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Se agrega cache 1.1
app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
