using VentasETL.Presetacion.WorkerServices;
using VentasETL.Infraestructure.Persistences.ConfigurationLayer;
using VentasETL.Aplication.LayerServices;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddRegistrationLayerPersistences(builder.Configuration);
builder.Services.AddLayerService();
builder.Services.AddHostedService<Worker>();



var host = builder.Build();
host.Run();
