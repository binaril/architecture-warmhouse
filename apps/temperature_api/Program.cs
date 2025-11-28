var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/temperature/{id}", (string id) =>
    {
        return new
        {
            Value = new Random().Next(10, 30),
            Unit = "C",
            Timestamp = DateTime.UtcNow,
            Location = "Room",
            Status = "OK",
            SensorID = Guid.NewGuid().ToString(),
            SensorType = "Temperature",
            Description = $"Temperature: {id}"
        };
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

