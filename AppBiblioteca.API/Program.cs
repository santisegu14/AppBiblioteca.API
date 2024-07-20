using AppBiblioteca.DataAccess.Data;
using AppBiblioteca.DataAccess.Inicializador;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = //Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")??
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString));
    //"Server =mssql; Database = appBibliotecaDoc; TrustServerCertificate = True; Trusted_Connection = True; MultipleActiveResultSets = true "));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

app.UseAuthorization();

// Aplicar Migraciones y Datos Iniciales
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var inicializador = services.GetRequiredService<IDbInicializador>();
        inicializador.Inicializar();
    }
    catch (Exception ex)
    {

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Un Error ocurrio al ejecutar la migracion");
    }
}
*/
app.MapControllers();
/*
var scope = app.Services.CreateScope();
await Migrations(scope.ServiceProvider);
*/

app.Run();
/*
async Task Migrations(IServiceProvider servicesProvider)
{
    var context= servicesProvider.GetService<ApplicationDbContext>();
    var conn_appdb = context.Database.GetDbConnection();

    context.Database.Migrate();

}
*/