var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container
builder.Services.AddControllers();
builder.Services.AddSingleton<AzureComputerVisionService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
  {
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
  });
});

var app = builder.Build();

// Configurar o pipeline HTTP
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Middleware de CORS deve estar antes do UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();




