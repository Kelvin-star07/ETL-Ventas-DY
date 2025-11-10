using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasETL.Infraestructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class ComplementProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Dimensiones");

            migrationBuilder.EnsureSchema(
                name: "Hechos");

            migrationBuilder.CreateTable(
                name: "DimCliente",
                schema: "Dimensiones",
                columns: table => new
                {
                    ClienteKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Segmento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimCliente", x => x.ClienteKey);
                });

            migrationBuilder.CreateTable(
                name: "DimProducto",
                schema: "Dimensiones",
                columns: table => new
                {
                    ProductKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodigoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimProducto", x => x.ProductKey);
                });

            migrationBuilder.CreateTable(
                name: "DimRegion",
                schema: "Dimensiones",
                columns: table => new
                {
                    RegionKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimRegion", x => x.RegionKey);
                });

            migrationBuilder.CreateTable(
                name: "DimTiempo",
                schema: "Dimensiones",
                columns: table => new
                {
                    TiempoKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompleta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    NombreMes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semana = table.Column<int>(type: "int", nullable: false),
                    Trimestre = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimTiempo", x => x.TiempoKey);
                });

            migrationBuilder.CreateTable(
                name: "FactVentas",
                schema: "Hechos",
                columns: table => new
                {
                    VentaKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiempoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(35,4)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVenta = table.Column<decimal>(type: "decimal(35,4)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Margen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroTransaccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactVentas", x => x.VentaKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DimCliente",
                schema: "Dimensiones");

            migrationBuilder.DropTable(
                name: "DimProducto",
                schema: "Dimensiones");

            migrationBuilder.DropTable(
                name: "DimRegion",
                schema: "Dimensiones");

            migrationBuilder.DropTable(
                name: "DimTiempo",
                schema: "Dimensiones");

            migrationBuilder.DropTable(
                name: "FactVentas",
                schema: "Hechos");
        }
    }
}
