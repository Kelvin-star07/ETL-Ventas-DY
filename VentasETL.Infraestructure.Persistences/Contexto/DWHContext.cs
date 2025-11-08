

using Microsoft.EntityFrameworkCore;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Entities.Destination.Dimensions;
using VentasETL.Domain.Entities.Destination.Facts;

namespace VentasETL.Infraestructure.Persistences.Contexto
{
    public class DWHContext : DbContext
    {
        public DWHContext(DbContextOptions<DWHContext> opt) : base(opt) { }


        public DbSet<FactVentas> FactVentas { get; set; }
        public DbSet<DimCliente> DimClientes { get; set; }
        public DbSet<DimProduct> DimProductos { get; set; }
        public DbSet<DimTiempo> DimTiempos { get; set; }
        public DbSet<DimRegion> DimRegiones { get; set; }



        //Dbset para lectura de datos
        string esquemaDim = "Dimensiones";
        string esquemaFact = "Hechos";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<FactVentas>().ToTable("FactVentas",esquemaFact);
            modelBuilder.Entity<DimProduct>().ToTable("DimProducto",esquemaDim);
            modelBuilder.Entity<DimCliente>().ToTable("DimCliente",esquemaDim);
            modelBuilder.Entity<DimRegion>().ToTable("DimRegion", esquemaDim);
            modelBuilder.Entity<DimTiempo>().ToTable("DimTiempo", esquemaDim);

            // DimProduct
            modelBuilder.Entity<DimProduct>()
                .Property(p => p.PrecioBase)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DimProduct>()
                .Property(p => p.PrecioUnitario)
                .HasColumnType("decimal(18,2)");

            // FactVentas
            modelBuilder.Entity<FactVentas>()
                .Property(f => f.Costo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FactVentas>()
                .Property(f => f.Descuento)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FactVentas>()
                .Property(f => f.Margen)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FactVentas>()
               .Property(f => f.PrecioUnitario)
               .HasColumnType("decimal(35,4)");

            modelBuilder.Entity<FactVentas>()
                .Property(f => f.TotalVenta)
                .HasColumnType("decimal(35,4)");
            base.OnModelCreating(modelBuilder);

        }
       
       
    }
}








   
