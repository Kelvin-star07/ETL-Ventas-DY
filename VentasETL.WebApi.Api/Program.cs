using Microsoft.AspNetCore.Builder;
using VentasETL.Infraestructure.Persistences.ConfigurationLayer;
using VentasETL.WebApi.Api.Data.LayerRegistrationApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddLayerApi(builder.Configuration);
builder.Services.AddRegistrationLayerPersistences(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventas ETL API v1");
        c.RoutePrefix = string.Empty; // para que se abra directamente en la raíz
    });

};



app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
