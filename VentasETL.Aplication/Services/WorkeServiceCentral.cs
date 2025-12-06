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
            // 1. EXTRACT
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
            var dimTiempos = MapDimTiempo(fechas, orders);
            var dimRegiones = MapDimRegion(dimClientes, clientesApi);

            dimClientes = await _dimClienteRepo.AddRangeReturnAsync(dimClientes);
            dimProductos = await _dimProductRepo.AddRangeReturnAsync(dimProductos);
            dimTiempos = await _dimTiempoRepo.AddRangeReturnAsync(dimTiempos);
            dimRegiones = await _dimRegionRepo.AddRangeReturnAsync(dimRegiones);

         
            var factVentas = MapFactVentas(
                orders,
                orderDetails,
                dimClientes,
                dimProductos,
                dimTiempos,
                dimRegiones
            );

          
            await _factVentasRepo.AddRangeReturnAsync(factVentas);
        }


        #region 🔄 Map Methods (Transform)

        private List<DimCliente> MapClientes(List<CustumerDto> csv,List<DataCustumerUpdatedDto> api)
        {
            var dict = new Dictionary<string, DimCliente>(StringComparer.OrdinalIgnoreCase);

          
            foreach (var c in csv)
            {
                var key = $"csv:{c.CustomerID}";

                dict[key] = new DimCliente
                {
                    CodigoCliente = key,
                    Nombre = $"{c.FirstName} {c.LastName}".Trim(),
                    Pais = c.Country ?? "N/A",
                    Region = "N/A",
                    Ciudad = c.City ?? "N/A",
                    TipoCliente = "CSV",
                    Segmento = "N/A",
                    FechaRegistro = DateTime.Now
                };
            }

         
            foreach (var a in api)
            {
               
                var apiKey =
                      !string.IsNullOrWhiteSpace(a.EmailAddress) ? $"email:{a.EmailAddress.ToLower()}"
                    : !string.IsNullOrWhiteSpace(a.PhoneNumber) ? $"phone:{a.PhoneNumber}"
                    : $"api:{a.FirstName}_{a.LastName}_{a.City}".ToLower();

              
                var matchCsv = csv.FirstOrDefault(c =>
                    (!string.IsNullOrWhiteSpace(c.Email) &&
                        c.Email.Equals(a.EmailAddress, StringComparison.OrdinalIgnoreCase))
                    ||
                    (!string.IsNullOrWhiteSpace(c.Phone) &&
                        c.Phone == a.PhoneNumber));

                if (matchCsv != null)
                {
                    var key = $"csv:{matchCsv.CustomerID}";
                    var cli = dict[key];

                    cli.Region = string.IsNullOrWhiteSpace(cli.Region) ? a.StateProvinceName : cli.Region;
                    cli.Pais = string.IsNullOrWhiteSpace(cli.Pais) ? a.CountryRegionName : cli.Pais;
                    cli.Segmento = string.IsNullOrWhiteSpace(cli.Segmento) ? a.AddressType : cli.Segmento;
                }
                else if (!dict.ContainsKey(apiKey))
                {
                    dict[apiKey] = new DimCliente
                    {
                        CodigoCliente = apiKey,
                        Nombre = $"{a.FirstName} {a.LastName}".Trim(),
                        Pais = a.CountryRegionName ?? "N/A",
                        Region = a.StateProvinceName ?? "N/A",
                        Ciudad = a.City ?? "N/A",
                        Segmento = a.AddressType ?? "N/A",
                        TipoCliente = "API",
                        FechaRegistro = DateTime.Now
                    };
                }
            }

            return dict.Values.ToList();
        }





        private List<DimProduct> MapProductos(List<ProductDto> csv,List<DataProductUpdatedDto> api,List<ProductDescriptionDto> descriptions)
        {
            var dict = new Dictionary<string, DimProduct>(StringComparer.OrdinalIgnoreCase);

            string defaultDescription =
                descriptions.FirstOrDefault()?.Description ?? "N/A";

            foreach (var a in api)
            {
                var key = $"api:{a.ProductId}";

                dict[key] = new DimProduct
                {
                    CodigoProducto = key,
                    Nombre = a.Name ?? "N/A",
                    Categoria = "N/A",
                    Descripcion = defaultDescription,
                    PrecioUnitario = a.ListPrice,
                    PrecioBase = a.StandardCost,
                    Estado = "Activo"
                };
            }

            
            foreach (var p in csv)
            {
                var key = p.ProductID > 0
                    ? $"api:{p.ProductID}"
                    : $"csv:{p.ProductName.ToLower()}";

                if (dict.TryGetValue(key, out var exist))
                {
                    exist.Categoria = exist.Categoria == "N/A" ? p.Category : exist.Categoria;
                    exist.PrecioUnitario = exist.PrecioUnitario == 0 ? p.Price : exist.PrecioUnitario;
                    exist.PrecioBase = exist.PrecioBase == 0 ? p.Price : exist.PrecioBase;
                }
                else
                {
                    dict[key] = new DimProduct
                    {
                        CodigoProducto = key,
                        Nombre = p.ProductName,
                        Categoria = p.Category,
                        Descripcion = defaultDescription,
                        PrecioUnitario = p.Price,
                        PrecioBase = p.Price,
                        Estado = "Activo"
                    };
                }
            }

            return dict.Values.ToList();
        }





        private List<DimTiempo> MapDimTiempo(List<HistoricalDataDto> historial, List<OrderDto> orders)
        {
            var dates = historial.Select(h => h.TransactionDate.Date)
                       .Union(orders.Select(o => o.OrderDate.Date))
                       .Distinct()
                       .OrderBy(d => d);
            return dates.Select(d => new DimTiempo
            {
                FechaCompleta = d,
                Anio = d.Year,
                Mes = d.Month,
                Dia = d.Day,
                NombreMes = d.ToString("MMMM", CultureInfo.InvariantCulture),
                Semana = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                DiaSemana = d.DayOfWeek.ToString(),
                Trimestre = (d.Month - 1) / 3 + 1
            }).ToList();
        }



        private List<DimRegion> MapDimRegion(List<DimCliente> clientes,List<DataCustumerUpdatedDto> api)
        {
            var dict = new Dictionary<string, DimRegion>(StringComparer.OrdinalIgnoreCase);

            foreach (var c in clientes)
            {
                string pais = c.Pais ?? "N/A";
                string region = c.Region ?? "N/A";
                string ciudad = c.Ciudad ?? "N/A";

                var key = $"{pais}|{region}|{ciudad}".ToLower();

                if (!dict.ContainsKey(key))
                {
                    var apiMatch = api.FirstOrDefault(a =>
                        a.City.Equals(ciudad, StringComparison.OrdinalIgnoreCase));

                    dict[key] = new DimRegion
                    {
                        Pais = pais,
                        Region = region != "N/A" ? region : (apiMatch?.StateProvinceName ?? "N/A"),
                        Ciudad = ciudad,
                        CodigoPostal = apiMatch?.PostalCode ?? "N/A",
                        Zona = apiMatch?.StateProvinceName ?? "N/A"
                    };
                }
            }

            return dict.Values.ToList();
        }





        private List<FactVentas> MapFactVentas(List<OrderDto> orders,List<OrderDetailDto> details,List<DimCliente> clientes,List<DimProduct> productos,List<DimTiempo> tiempos,List<DimRegion> regiones)
        {
            var clienteLookup = clientes.ToDictionary(x => x.CodigoCliente, x => x);
            var prodLookup = productos.ToDictionary(x => x.CodigoProducto, x => x);
            var tiempoLookup = tiempos.ToDictionary(x => x.FechaCompleta.Date, x => x);
            var regionLookup = regiones.ToDictionary(
                x => $"{x.Pais}|{x.Region}|{x.Ciudad}".ToLower(), x => x);

            var fact = new List<FactVentas>();

            foreach (var o in orders)
            {
                if (!tiempoLookup.TryGetValue(o.OrderDate.Date, out var t))
                    continue;

                string ck = $"csv:{o.CustomerID}";
                if (!clienteLookup.TryGetValue(ck, out var cli))
                    continue;

                string rKey = $"{cli.Pais}|{cli.Region}|{cli.Ciudad}".ToLower();
                if (!regionLookup.TryGetValue(rKey, out var reg))
                    continue;

                foreach (var d in details.Where(x => x.OrderID == o.OrderID))
                {
                    string pKey = $"api:{d.ProductID}";
                    if (!prodLookup.TryGetValue(pKey, out var prod))
                        continue;

                    int cantidad = Math.Max(0, d.Quantity);
                    decimal precioUnit = cantidad > 0 ? d.TotalPrice / cantidad : d.TotalPrice;

                    fact.Add(new FactVentas
                    {
                        TiempoId = t.TiempoKey,
                        ProductoId = prod.ProductKey,
                        ClienteId = cli.ClienteKey,
                        RegionId = reg.RegionKey,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnit,
                        Descuento = 0,
                        TotalVenta = d.TotalPrice,
                        Costo = prod.PrecioBase,
                        Margen = d.TotalPrice - prod.PrecioBase * cantidad,
                        NumeroTransaccion = o.OrderID
                    });
                }
            }

            return fact;
        }



        #endregion
    }






}
