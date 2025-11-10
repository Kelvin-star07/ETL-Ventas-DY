
using System.Globalization;
using VentasETL.Aplication.Dtos.Source.Api;
using VentasETL.Aplication.Dtos.Source.CSV.Custumer;
using VentasETL.Aplication.Dtos.Source.CSV.Order;
using VentasETL.Aplication.Dtos.Source.CSV.OrderDetail;
using VentasETL.Aplication.Dtos.Source.CSV.Product;
using VentasETL.Aplication.Dtos.Source.DB;
using VentasETL.Aplication.Interfaces;
using VentasETL.Aplication.Interfaces.Destination;
using VentasETL.Aplication.Interfaces.Source.Api;
using VentasETL.Aplication.Interfaces.Source.CSV;
using VentasETL.Aplication.Interfaces.Source.DB;
using VentasETL.Domain.Entities.Destination.Dimensions;
using VentasETL.Domain.Entities.Destination.Facts;

namespace VentasETL.Aplication.Services
{
   
    public class WorkeServiceCentral : IWorkeServiceFinal
    {
        private readonly ICustumerService _custumerCsvService;
        private readonly IReadDataCustumerApiService<DataCustumerUpdatedDto> _custumerApiService;
        private readonly IOrderService _orderCsvService;
        private readonly IOrderDetailService _orderDetailCsvService;
        private readonly IProductService _productCsvService;
        private readonly IReadDataProductService<DataProductUpdatedDto> _productApiService;
        private readonly IProducDescriptionService _productDescriptionService;
        private readonly IHistoricalDataService _historicalDataService;

        private readonly IGenericDetinationService<DimCliente> _dimClienteRepo;
        private readonly IGenericDetinationService<DimProduct> _dimProductRepo;
        private readonly IGenericDetinationService<DimTiempo> _dimTiempoRepo;
        private readonly IGenericDetinationService<DimRegion> _dimRegionRepo;
        private readonly IGenericDetinationService<FactVentas> _factVentasRepo;

        public WorkeServiceCentral(
            ICustumerService custumerCsvService,
            IReadDataCustumerApiService<DataCustumerUpdatedDto> custumerApiService,
            IOrderService orderCsvService,
            IOrderDetailService orderDetailCsvService,
            IProductService productCsvService,
            IReadDataProductService<DataProductUpdatedDto> productApiService,
            IProducDescriptionService productDescriptionService,
            IHistoricalDataService historicalDataService,
            IGenericDetinationService<DimCliente> dimClienteRepo,
            IGenericDetinationService<DimProduct> dimProductRepo,
            IGenericDetinationService<DimTiempo> dimTiempoRepo,
            IGenericDetinationService<DimRegion> dimRegionRepo,
            IGenericDetinationService<FactVentas> factVentasRepo)
        {
            _custumerCsvService = custumerCsvService;
            _custumerApiService = custumerApiService;
            _orderCsvService = orderCsvService;
            _orderDetailCsvService = orderDetailCsvService;
            _productCsvService = productCsvService;
            _productApiService = productApiService;
            _productDescriptionService = productDescriptionService;
            _historicalDataService = historicalDataService;

            _dimClienteRepo = dimClienteRepo;
            _dimProductRepo = dimProductRepo;
            _dimTiempoRepo = dimTiempoRepo;
            _dimRegionRepo = dimRegionRepo;
            _factVentasRepo = factVentasRepo;
        }

        public async Task RunETLAsync()
        {
          
            var clientesCsv = (await _custumerCsvService.GetAllAsync()).ToList();
            var clientesApi = (await _custumerApiService.GetAllAsync()).ToList();
            var orders = (await _orderCsvService.GetAllAsync()).ToList();
            var orderDetails = (await _orderDetailCsvService.GetAllAsync()).ToList();
            var productosCsv = (await _productCsvService.GetAllAsync()).ToList();
            var productosApi = (await _productApiService.GetAllAsync()).ToList();
            var descriptions = (await _productDescriptionService.ReadData()).ToList();
            var fechas = (await _historicalDataService.ReadData()).ToList();

            var dimClientes = MapClientes(clientesCsv, clientesApi);
            var dimProductos = MapProductos(productosCsv, productosApi, descriptions);
            var dimTiempos = MapDimTiempo(fechas,orders);
            var dimRegiones = MapDimRegion(dimClientes, clientesApi);
            var factVentas = MapFactVentas(orders, orderDetails, dimClientes, dimProductos, dimTiempos, dimRegiones);

           
            await _dimClienteRepo.AddRangeAsync(dimClientes);
            await _dimProductRepo.AddRangeAsync(dimProductos);
            await _dimTiempoRepo.AddRangeAsync(dimTiempos);
            await _dimRegionRepo.AddRangeAsync(dimRegiones);
            await _factVentasRepo.AddRangeAsync(factVentas);
        }

        #region 🔄 Map Methods (Transform)

        private List<DimCliente> MapClientes(List<CustumerDto> csv, List<DataCustumerUpdatedDto> api)
        {
            var clientes = new List<DimCliente>();

            // CSV
            clientes.AddRange(csv.Select(c => new DimCliente
            {
                CodigoCliente = Guid.NewGuid().ToString(),
                Nombre = $"{c.FirstName} {c.LastName}",
                TipoCliente = "N/A",
                Genero = "N/A",
                Edad = 0,
                Pais = c.Country ?? "N/A",
                Ciudad = c.City ?? "N/A",
                Region = "N/A",
                Segmento = "N/A",
                FechaRegistro = DateTime.Now
            }));

            // API
            clientes.AddRange(api.Select(c => new DimCliente
            {
                CodigoCliente = Guid.NewGuid().ToString(),
                Nombre = $"{c.FirstName} {c.LastName}",
                TipoCliente = "API",
                Genero = "N/A",
                Edad = 0,
                Pais = c.CountryRegionName ?? "N/A",
                Ciudad = c.City ?? "N/A",
                Region = c.StateProvinceName ?? "N/A",
                Segmento = c.AddressType ?? "N/A",
                FechaRegistro = DateTime.Now
            }));

            return clientes
                .GroupBy(c => c.Nombre)
                .Select(g => g.First())
                .ToList();
        }



        private List<DimProduct> MapProductos(List<ProductDto> csv, List<DataProductUpdatedDto> api, List<ProductDescriptionDto> descriptions)
        {
            var productos = new List<DimProduct>();

            foreach (var apiProd in api)
            {
                var csvProd = csv.FirstOrDefault(p => p.ProductName.Equals(apiProd.Name, StringComparison.OrdinalIgnoreCase));
                var desc = descriptions.FirstOrDefault()?.Description ?? "N/A";

                productos.Add(new DimProduct
                {
                    CodigoProducto = apiProd.ProductNumber ?? Guid.NewGuid().ToString(),
                    Nombre = apiProd.Name,
                    Descripcion = desc,
                    Categoria = csvProd?.Category ?? "N/A",
                    SubCategoria = "N/A",
                    Marca = "N/A",
                    PrecioUnitario = apiProd.ListPrice,
                    PrecioBase = apiProd.StandardCost,
                    Proveedor = "N/A",
                    Estado = "Activo"
                });
            }

           
            var faltantes = csv
                .Where(p => !productos.Any(x => x.Nombre == p.ProductName))
                .Select(p => new DimProduct
                {
                    CodigoProducto = Guid.NewGuid().ToString(),
                    Nombre = p.ProductName,
                    Descripcion = descriptions.FirstOrDefault()?.Description ?? "N/A",
                    Categoria = p.Category,
                    PrecioUnitario = p.Price,
                    PrecioBase = p.Price,
                    Estado = "Activo",
                    Proveedor = "N/A",
                    Marca = "N/A"
                });

            productos.AddRange(faltantes);
            return productos.GroupBy(p => p.CodigoProducto).Select(g => g.First()).ToList();
        }



        private List<DimTiempo> MapDimTiempo(List<HistoricalDataDto> fechas, List<OrderDto> orders)
        {
            
            var allDates = fechas.Select(f => f.TransactionDate.Date)
                                 .Union(orders.Select(o => o.OrderDate.Date))
                                 .Distinct()
                                 .OrderBy(d => d)
                                 .ToList();

            var tiempos = allDates.Select((date, index) => new DimTiempo
            {
                TiempoKey = index + 1, 
                FechaCompleta = date,
                Anio = date.Year,
                Mes = date.Month,
                Dia = date.Day,
                Trimestre = (date.Month - 1) / 3 + 1,
                NombreMes = date.ToString("MMMM", CultureInfo.InvariantCulture),
                Semana = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                DiaSemana = date.DayOfWeek.ToString()
            }).ToList();

            return tiempos;
        }



        private List<DimRegion> MapDimRegion(List<DimCliente> clientes, List<DataCustumerUpdatedDto> apiClientes)
        {
            var regiones = clientes.Select(c => new DimRegion
            {
                Pais = c.Pais,
                Region = c.Region,
                Ciudad = c.Ciudad,
                CodigoPostal = apiClientes.FirstOrDefault(a => a.City == c.Ciudad)?.PostalCode ?? "N/A",
                Zona = apiClientes.FirstOrDefault(a => a.City == c.Ciudad)?.StateProvinceName ?? "N/A"
            });

            return regiones.GroupBy(r => new { r.Pais, r.Region, r.Ciudad }).Select(g => g.First()).ToList();
        }


        private List<FactVentas> MapFactVentas(
    List<OrderDto> orders,
    List<OrderDetailDto> details,
    List<DimCliente> clientes,
    List<DimProduct> productos,
    List<DimTiempo> tiempos,
    List<DimRegion> regiones)
        {
            var fact = new List<FactVentas>();

            foreach (var order in orders)
            {
                var cliente = clientes.FirstOrDefault(c => c.CodigoCliente == order.CustomerID.ToString());
                var tiempo = tiempos.FirstOrDefault(t => t.FechaCompleta.Date == order.OrderDate.Date);

                var det = details.Where(d => d.OrderID == order.OrderID);
                foreach (var d in det)
                {
                    var producto = productos.FirstOrDefault(p => p.CodigoProducto == d.ProductID.ToString());
                    var region = regiones.FirstOrDefault(r => r.Pais == (cliente?.Pais ?? "N/A"));

                    fact.Add(new FactVentas
                    {
                        ClienteId = cliente?.ClienteKey ?? 0,
                        ProductoId = producto?.ProductKey ?? 0,
                        RegionId = region?.RegionKey ?? 0,
                        TiempoId = tiempo?.TiempoKey ?? 0,
                        Cantidad = d.Quantity,
                        PrecioUnitario = d.TotalPrice / Math.Max(d.Quantity, 1),
                        TotalVenta = d.TotalPrice,
                        Costo = producto?.PrecioBase ?? 0,
                        Margen = d.TotalPrice - (producto?.PrecioBase ?? 0) * d.Quantity,
                        NumeroTransaccion = order.OrderID
                    });
                }
            }

            return fact;
        }

        #endregion
    }






}
