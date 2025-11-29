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

app.MapGet("/temperature/{id}", (string? id, string? location) =>
    {
        if (string.IsNullOrEmpty(id))
        {
	        id = location switch
	        {
		        "Living Room" => "1",
		        "Bedroom" => "2",
		        "Kitchen" => "3",
		        _ => "0"
	        };
        }
        
        if (string.IsNullOrEmpty(location))
        {
	        location = id switch
	        {
		        "1" => "Living Room",
		        "2" => "Bedroom",
		        "3" => "Kitchen",
		        _ => "Unknown"
	        };
        }
        
        return new
        {
            Value = new Random().Next(10, 30),
            Unit = "C",
            Timestamp = DateTime.UtcNow,
            Location = location,
            Status = "OK",
            SensorID = id,
            SensorType = "Temperature",
            Description = $"Temperature: {id}"
        };
    })
    .WithName("Temperature")
    .WithOpenApi();

app.Run();

