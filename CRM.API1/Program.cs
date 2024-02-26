using CRM.API1.Endorpoints;
using CRM.API1.Models.DAL;
using Microsoft.EntityFrameworkCore;

//crear un nuev constructor de la ap web.
var builder = WebApplication.CreateBuilder(args);

//agregar servicios para habilitar la generacion de documentaciom
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configurar y agregar un conteexto de base de datos para Entity
builder.Services.AddDbContext<CRMContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);

//agregar una instancia de la clase customerDAL como un servicio
builder.Services.AddScoped<CostumerDAL>();

//contruye la app web.
var app = builder.Build();

//agregar los puntos finales relacionados con los clientes de la aplicacion
app.AddCustomerEndopoints();


//Verificar si la app se esta ejecutando en un entorno
if (app.Environment.IsDevelopment())
{
    //Habilitar el uso de Swagger para la documentacion.
    app.UseSwagger();
    app.UseSwaggerUI();
}

//agregar middleware para redigirir las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();


//ejecutar la app
app.Run();
