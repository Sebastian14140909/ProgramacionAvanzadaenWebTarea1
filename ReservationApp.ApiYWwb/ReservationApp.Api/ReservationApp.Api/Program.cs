using ReservationApp.Api.DTOs;
using ReservationApp.Api.Middleware;
using ReservationApp.Api.Repository;
using ReservationApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddControllers();
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

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

// Minimal Api para hacer get all a la base de datos
app.MapGet("/api/minimal/reservations", async (IReservationService service) =>
{
    var reservations = await service.GetReservationsAsync();
    return Results.Ok(reservations);
});

// Minimal Api para hacer post/create a la base de datos
app.MapPost("/api/minimal/reservations", async (CreateReservationDTO dto, IReservationService service) =>
{
    await service.CreateReservationAsync(dto);
    return Results.Ok("Reservación creada exitosamente.");
});

app.Run();

