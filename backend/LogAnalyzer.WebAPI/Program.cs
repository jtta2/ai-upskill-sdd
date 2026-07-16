var builder = WebApplication.CreateBuilder(args);

// Regisztráljuk a controllereket a rendszerbe
builder.Services.AddControllers();

var app = builder.Build();

// Engedélyezzük a controllerek útvonalválasztását (routing)
app.MapControllers();

app.Run();