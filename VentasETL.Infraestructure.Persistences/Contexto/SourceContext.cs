using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Api;
using VentasETL.Domain.Entities.DBRead;

namespace VentasETL.Infraestructure.Persistences.Contexto
{
    public class SourceContext : DbContext
    {

        public SourceContext(DbContextOptions<SourceContext> opts) : base(opts) { }


        public DbSet<HistoricalData> HistoricalData { get; set; }

        public DbSet<ProductDescription> ProductDescriptions { get; set; }

        public DbSet<SalesHistoricalData> SalesHistoricalData { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataCustumerUpdate>()
                .HasNoKey() 
                .ToView("HistoricalData", "dbo");

            modelBuilder.Entity<ProductDescription>()
                .HasNoKey() 
                .ToView("ProductDescription", "dbo");

            modelBuilder.Entity<SalesHistoricalData>()
                .HasNoKey() 
                .ToView("SalesHistoricalData", "dbo");

            base.OnModelCreating(modelBuilder);

        }

    }
}
