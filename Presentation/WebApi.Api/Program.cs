var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false) // Opsiyonel olmamasinin sebebi her turlu appsettings gorulmek istenebilir.
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true); // Opsiyonel olmasinin sebebi Development veya Production ortami olarak iki ayri ortam kullanilabilmesidir.


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
