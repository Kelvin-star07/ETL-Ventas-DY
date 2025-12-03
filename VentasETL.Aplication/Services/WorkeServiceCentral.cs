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
            var dimTiempos = MapDimTiempo(fechas, orders);
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
            var dict = new Dictionary<string, DimCliente>(StringComparer.OrdinalIgnoreCase);

            foreach (var c in csv)
            {
                var key = c.CustomerID.ToString();
                if (!dict.ContainsKey(key))
                {
                    dict[key] = new DimCliente
                    {
                        CodigoCliente = key,
                        Nombre = $"{c.FirstName} {c.LastName}".Trim(),
                        Pais = c.Country ?? "N/A",
                        Ciudad = c.City ?? "N/A",
                        TipoCliente = "CSV",
                        FechaRegistro = DateTime.Now
                    };
                }
            }

            foreach (var a in api)
            {
                string apiKey = !string.IsNullOrWhiteSpace(a.EmailAddress) ? a.EmailAddress.ToLowerInvariant()
                              : !string.IsNullOrWhiteSpace(a.PhoneNumber) ? $"phone:{a.PhoneNumber}"
                              : $"{a.FirstName}_{a.LastName}_{a.City}".ToLowerInvariant();


                var matchCsv = csv.FirstOrDefault(c =>
                    (!string.IsNullOrWhiteSpace(c.Email) && c.Email.Equals(a.EmailAddress, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(c.Phone) && c.Phone == a.PhoneNumber));
                if (matchCsv != null)
                {
                    var key = matchCsv.CustomerID.ToString();
                    var existing = dict[key];
                    existing.Region = string.IsNullOrWhiteSpace(existing.Region) ? a.StateProvinceName : existing.Region;
                    existing.Segmento = string.IsNullOrWhiteSpace(existing.Segmento) ? a.AddressType : existing.Segmento;
                    existing.Pais = string.IsNullOrWhiteSpace(existing.Pais) ? a.CountryRegionName : existing.Pais;
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
                        TipoCliente = "API",
                        Segmento = a.AddressType ?? "N/A",
                        FechaRegistro = DateTime.Now
                    };
                }
            }

            return dict.Values.ToList();
        }





        private List<DimProduct> MapProductos(List<ProductDto> csv, List<DataProductUpdatedDto> api, List<ProductDescriptionDto> descriptions)
        {
            var dict = new Dictionary<string, DimProduct>(StringComparer.OrdinalIgnoreCase);


            foreach (var a in api)
            {
                var key = a.ProductId.ToString();
                var desc = descriptions.FirstOrDefault(d => false)?.Description
                           ?? descriptions.FirstOrDefault()?.Description ?? "N/A";
                dict[key] = new DimProduct
                {
                    CodigoProducto = key,
                    Nombre = a.Name ?? "N/A",
                    Categoria = "N/A",
                    Descripcion = desc,
                    PrecioUnitario = a.ListPrice,
                    PrecioBase = a.StandardCost,
                    Estado = "Activo"
                };
            }


            foreach (var p in csv)
            {
                var key = p.ProductID > 0 ? p.ProductID.ToString() : p.ProductName;
                if (dict.TryGetValue(key, out var existing))
                {
                    existing.Categoria = string.IsNullOrWhiteSpace(existing.Categoria) || existing.Categoria == "N/A" ? p.Category : existing.Categoria;
                    existing.PrecioUnitario = existing.PrecioUnitario == 0 ? p.Price : existing.PrecioUnitario;
                    existing.PrecioBase = existing.PrecioBase == 0 ? p.Price : existing.PrecioBase;
                }
                else
                {
                    dict[key] = new DimProduct
                    {
                        CodigoProducto = key,
                        Nombre = p.ProductName,
                        Categoria = p.Category,
                        Descripcion = descriptions.FirstOrDefault()?.Description ?? "N/A",
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




        private List<DimRegion> MapDimRegion(List<DimCliente> clientesStaging, List<DataCustumerUpdatedDto> api)
        {
            var dict = new Dictionary<string, DimRegion>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in clientesStaging)
            {
                var key = $"{(c.Pais ?? "N/A").ToLowerInvariant()}|{(c.Region ?? "N/A").ToLowerInvariant()}|{(c.Ciudad ?? "N/A").ToLowerInvariant()}";
                if (!dict.ContainsKey(key))
                {
                    var postal = api.FirstOrDefault(a => (a.City ?? "").Equals(c.Ciudad, StringComparison.OrdinalIgnoreCase))?.PostalCode ?? "N/A";
                    var zona = api.FirstOrDefault(a => (a.City ?? "").Equals(c.Ciudad, StringComparison.OrdinalIgnoreCase))?.StateProvinceName ?? "N/A";
                    dict[key] = new DimRegion { Pais = c.Pais!, Region = c.Region!, Ciudad = c.Ciudad!, CodigoPostal = postal, Zona = zona };
                }
            }
            return dict.Values.ToList();
        }





        private List<FactVentas> MapFactVentas(List<OrderDto> orders, List<OrderDetailDto> details, List<DimCliente> persistedClientes, List<DimProduct> persistedProductos, List<DimTiempo> persistedTiempos, List<DimRegion> persistedRegiones)
        {
            var clienteByBusiness = persistedClientes.ToDictionary(c => c.CodigoCliente, c => c);
            var productoByBusiness = persistedProductos.ToDictionary(p => p.CodigoProducto, p => p);
            var tiempoByDate = persistedTiempos.ToDictionary(t => t.FechaCompleta.Date, t => t);
            var regionByComposite = persistedRegiones.ToDictionary(r => $"{r.Pais}|{r.Region}|{r.Ciudad}".ToLowerInvariant(), r => r);

            var fact = new List<FactVentas>();
            foreach (var order in orders)
            {
                var t = tiempoByDate.GetValueOrDefault(order.OrderDate.Date);
                string clienteKey = order.CustomerID.ToString();
                clienteByBusiness.TryGetValue(clienteKey, out var cliente);
                foreach (var d in details.Where(x => x.OrderID == order.OrderID))
                {
                    var productKey = d.ProductID.ToString();
                    productoByBusiness.TryGetValue(productKey, out var prod);


                    DimRegion region = null;
                    if (cliente != null)
                    {
                        var comp = $"{cliente.Pais}|{cliente.Region}|{cliente.Ciudad}".ToLowerInvariant();
                        regionByComposite.TryGetValue(comp, out region);
                    }

                    var costo = prod?.PrecioBase ?? 0m;
                    var cantidad = Math.Max(0, d.Quantity);
                    var precioUnit = cantidad > 0 ? d.TotalPrice / cantidad : d.TotalPrice;

                    fact.Add(new FactVentas
                    {
                        TiempoId = t?.TiempoKey ?? 0,
                        ProductoId = prod?.ProductKey ?? 0,
                        ClienteId = cliente?.ClienteKey ?? 0,
                        RegionId = region?.RegionKey ?? 0,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnit,
                        Descuento = 0m,
                        TotalVenta = d.TotalPrice,
                        Costo = costo,
                        Margen = d.TotalPrice - costo * cantidad,
                        NumeroTransaccion = order.OrderID
                    });
                }
            }
            return fact;
        }


        #endregion
    }






}
