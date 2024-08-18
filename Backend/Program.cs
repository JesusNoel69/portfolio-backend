using Backend.Controllers;
using dotenv.net;

var success=DotEnv.AutoConfig();
if(success){
    DotEnv.Load();
    Console.WriteLine(Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY"));
}

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // Configura el puerto 80
    serverOptions.ListenAnyIP(5024); // Configura el puerto 5024
});
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name : "AllowAnyOriginPolicy",
        builder =>
        {builder.WithOrigins("https://master--portfolio-jesus-n.netlify.app")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});
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
//aceptar peticiones de angular
//las politicas permiten definir reglas de los puntos de origen 
app.UseCors("AllowAnyOriginPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
