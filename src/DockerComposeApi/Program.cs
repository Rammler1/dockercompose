using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// DB-Test-Endpunkt
app.MapGet("/db-test", async () =>
{
    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                           ?? builder.Configuration.GetConnectionString("DefaultConnection");

    try
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        return Results.Ok(new { status = "Erfolgreich mit der SQL-Datenbank verbunden!" });
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message, title: "Datenbankverbindung fehlgeschlagen");
    }
});

app.MapGet("/", () => "C# Docker API läuft!");

app.Run();
