using Freight.Domain.Interfaces;
using Freight.Infrastructure.Calculators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ViaCEP — cliente HTTP tipado
// builder.Services.AddHttpClient<ViaCepService>(client =>
// {
//     client.BaseAddress = new Uri("https://viacep.com.br");
//     client.Timeout = TimeSpan.FromSeconds(5);
// });

// Transportadoras — cada nova = apenas uma linha aqui
// O FreightService recebe IEnumerable<IFreightCalculator> automaticamente
builder.Services.AddScoped<IFreightCalculator, CorreiosCalculator>();
builder.Services.AddScoped<IFreightCalculator, JadLogCalculator>();
builder.Services.AddScoped<IFreightCalculator, LoggiCalculator>(); // ← nova transportadora


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/test", () =>
{
    return Results.Ok("Hello, World!"); // Apenas um teste, sem lógica real
})
.WithName("GetTest");

app.Run();