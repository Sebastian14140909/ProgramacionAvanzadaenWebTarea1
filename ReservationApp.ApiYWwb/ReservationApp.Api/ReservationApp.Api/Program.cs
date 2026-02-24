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

app.MapGet("/api/min/reservations", async (IReservationService service) =>
{
    var reservations = await service.GetReservationsAsync();
    return Results.Ok(reservations);
});


app.MapGet("/api/min/reservations/{id}", async (int id, IReservationService service) =>
{
    var reservation = await service.GetReservationByIdAsync(id);
    return reservation is not null
        ? Results.Ok(reservation)
        : Results.NotFound($"Reservación {id} no encontrada");
});


app.MapPost("/api/min/reservations", async (CreateReservationDTO dto, IReservationService service) =>
{
    await service.CreateReservationAsync(dto);
    return Results.Created("/api/min/reservations", dto);
});


app.MapPut("/api/min/reservations/{id}", async (int id, UpdateReservationDTO dto, IReservationService service) =>
{
    dto.Id = id;
    await service.UpdateReservationAsync(dto);
    return Results.NoContent();
});


app.MapDelete("/api/min/reservations/{id}", async (int id, IReservationService service) =>
{
    await service.DeleteReservationAsync(id);
    return Results.NoContent();
});

app.Run();

