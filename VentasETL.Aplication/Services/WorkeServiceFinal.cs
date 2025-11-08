using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Aplication.Dtos.Api;
using VentasETL.Aplication.Dtos.Source.Custumer;
using VentasETL.Aplication.Dtos.Source.Order;
using VentasETL.Aplication.Dtos.Source.OrderDetail;
using VentasETL.Aplication.Dtos.Source.Product;
using VentasETL.Aplication.Interfaces;
using VentasETL.Aplication.Interfaces.Api;
using VentasETL.Aplication.Interfaces.Destination;
using VentasETL.Domain.Entities.Destination.Dimensions;
using VentasETL.Domain.Entities.Destination.Facts;
using VentasETL.Domain.Interfaces.Source;

namespace VentasETL.Aplication.Services
{
    public class WorkeServiceFinal : IWorkeServiceFinal
    {




        private readonly ICustumerService _custumerCsvService;
        private readonly IReadDataCustumerApiService<DataCustumerUpdatedDto> _custumerApiService;
        private readonly IOrderService _orderCsvService;
        private readonly IOrderDetailService _orderDetailCsvService;
        private readonly IProductService _productCsvService;


        private readonly IReadDataProductService<DataProductUpdatedDto> _productApiService;
        private readonly IGenericDetinationService<DimCliente> _dimClienteRepo;
        private readonly IGenericDetinationService<DimProduct> _dimProductRepo;
        private readonly IGenericDetinationService<DimTiempo> _dimTiempoRepo;
        private readonly IGenericDetinationService<DimRegion> _dimRegionRepo;
        private readonly IGenericDetinationService<FactVentas> _factVentasRepo;

        public WorkeServiceFinal(
            ICustumerService custumerCsvService,
            IReadDataCustumerApiService<DataCustumerUpdatedDto> custumerApiService,
            IOrderService orderCsvService,
            IOrderDetailService orderDetailCsvService,
            IProductService productCsvService,
            IReadDataProductService<DataProductUpdatedDto> productApiService,
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


            var dimClientes = MapClientes(clientesCsv, clientesApi);
            await _dimClienteRepo.AddRangeAsync(dimClientes);


            var dimProductos = MapProductos(productosCsv, productosApi);
            await _dimProductRepo.AddRangeAsync(dimProductos);


            var dimTiempos = MapDimTiempo(orders.Select(o => o.OrderDate).ToList());
            await _dimTiempoRepo.AddRangeAsync(dimTiempos);


            var dimRegiones = MapDimRegion(dimClientes, dimProductos);
            await _dimRegionRepo.AddRangeAsync(dimRegiones);


            var factVentas = MapFactVentas(orders, orderDetails, dimClientes, dimProductos, dimTiempos, dimRegiones);
            await _factVentasRepo.AddRangeAsync(factVentas);
        }

        #region Métodos Privados de Mapeo

        private List<DimCliente> MapClientes(List<CustumerDto> clientesCsv, List<DataCustumerUpdatedDto> clientesApi)
        {
            var clientes = new List<DimCliente>();


            clientes.AddRange(clientesCsv.Select(c => new DimCliente
            {
                ClienteKey = 0,
                CodigoCliente = Guid.NewGuid().ToString(),
                Nombre = $"{c.FirstName} {c.LastName}",
                TipoCliente = "N/A",
                Genero = "N/A",
                Edad = 0,
                Pais = string.IsNullOrWhiteSpace(c.Country) ? "N/A" : c.Country,
                Ciudad = string.IsNullOrWhiteSpace(c.City) ? "N/A" : c.City,
                Region = "N/A",
                Segmento = "N/A",
                FechaRegistro = DateTime.Now
            }));


            clientes.AddRange(clientesApi.Select(c => new DimCliente
            {
                ClienteKey = 0,
                CodigoCliente = Guid.NewGuid().ToString(),
                TipoCliente = "N/A",
                Nombre = $"{c.FirstName} {c.LastName}",
                Pais = string.IsNullOrWhiteSpace(c.CountryRegionName) ? "N/A" : c.CountryRegionName,
                Ciudad = string.IsNullOrWhiteSpace(c.City) ? "N/A" : c.City,
                Region = string.IsNullOrWhiteSpace(c.StateProvinceName) ? "N/A" : c.StateProvinceName,
                Segmento = string.IsNullOrWhiteSpace(c.AddressType) ? "N/A" : c.AddressType,
                FechaRegistro = DateTime.Now
            }));

            return clientes.GroupBy(c => c.Nombre).Select(g => g.First()).ToList();
        }

        private List<DimProduct> MapProductos(List<ProductDto> productosCsv, List<DataProductUpdatedDto> productosApi)
        {
            var productos = new List<DimProduct>();

            productos.AddRange(productosCsv.Select(p => new DimProduct
            {
                ProductKey = p.ProductID,
                Descripcion = "N/A",
                CodigoProducto = Guid.NewGuid().ToString(), 
                Nombre = p.ProductName,
                Categoria = string.IsNullOrWhiteSpace(p.Category) ? "N/A" : p.Category,
                SubCategoria = "N/A",
                Marca = "N/A",
                PrecioUnitario = p.Price,
                PrecioBase = p.Price,
                Proveedor = "N/A",
                Estado = "Activo"
            }));


            productos.AddRange(productosApi.Select(p => new DimProduct
            {
                ProductKey = p.ProductId,
                Descripcion = "N.A",
                CodigoProducto = p.ProductNumber,
                Nombre = p.Name,
                SubCategoria = "N/A",
                Categoria = "N/A",
                Marca = "N/A",
                PrecioBase = p.StandardCost,
                PrecioUnitario = p.ListPrice,
                Proveedor = "N/A",
                Estado = "Activo"
            }));

            return productos.GroupBy(p => p.ProductKey).Select(g => g.First()).ToList();
        }

        private List<DimTiempo> MapDimTiempo(List<DateTime> fechas)
        {
            return fechas.Select(f => new DimTiempo
            {
                TiempoKey = 0,
                FechaCompleta = f,
                Anio = f.Year,
                Mes = f.Month,
                Dia = f.Day,
                Trimestre = f.Month + 2 /1,
                NombreMes = f.ToString("MMMM"),
                Semana = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(f, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
                DiaSemana = f.DayOfWeek.ToString()
            }).GroupBy(t => t.FechaCompleta).Select(g => g.First()).ToList();
        }

        private List<DimRegion> MapDimRegion(List<DimCliente> dimClientes, List<DimProduct> dimProductos)
        {
            var regiones = new List<DimRegion>();


            regiones.AddRange(dimClientes.Select(c => new DimRegion
            {
                RegionKey = 0,
                Pais = string.IsNullOrWhiteSpace(c.Pais) ? "N/A" : c.Pais,
                Region = string.IsNullOrWhiteSpace(c.Region) ? "N/A" : c.Region,
                Ciudad = string.IsNullOrWhiteSpace(c.Ciudad) ? "N/A" : c.Ciudad,
                CodigoPostal = "N/A",
                Zona = "N/A"
            }));


            return regiones.GroupBy(r => r.RegionKey).Select(g => g.First()).ToList();
        }

        private List<FactVentas> MapFactVentas(
            List<OrderDto> orders,
            List<OrderDetailDto> orderDetails,
            List<DimCliente> dimClientes,
            List<DimProduct> dimProductos,
            List<DimTiempo> dimTiempos,
            List<DimRegion> dimRegiones)
        {
            var factVentas = new List<FactVentas>();

            foreach (var order in orders)
            {
                var cliente = dimClientes.FirstOrDefault(c => c.ClienteKey == order.CustomerID);
                var tiempo = dimTiempos.FirstOrDefault(t => t.FechaCompleta.Date == order.OrderDate.Date);

                var detalles = orderDetails.Where(d => d.OrderID == order.OrderID);

                foreach (var detalle in detalles)
                {
                    var producto = dimProductos.FirstOrDefault(p => p.ProductKey == detalle.ProductID);

                    var region = dimRegiones.FirstOrDefault(r =>
                        r.Pais == (cliente?.Pais ?? "N/A") &&
                        r.Region == (cliente?.Region ?? "N/A") &&
                        r.Ciudad == (cliente?.Ciudad ?? "N/A"));

                    factVentas.Add(new FactVentas
                    {
                        VentaKey = 0,
                        ClienteId = cliente?.ClienteKey ?? 0,
                        ProductoId = producto?.ProductKey ?? 0,
                        RegionId = region?.RegionKey ?? 0,
                        TiempoId = tiempo?.TiempoKey ?? 0,
                        Cantidad = detalle.Quantity,
                        PrecioUnitario = detalle.TotalPrice / Math.Max(detalle.Quantity, 1),
                        TotalVenta = detalle.TotalPrice,
                        Descuento = 0,
                        Costo = producto?.PrecioBase ?? 0,
                        Margen = detalle.TotalPrice - (producto?.PrecioBase ?? 0) * detalle.Quantity,
                        NumeroTransaccion = order.OrderID
                    });
                }
            }

            return factVentas;
        }

        #endregion
    }




}
