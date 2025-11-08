using Microsoft.EntityFrameworkCore;
using VentasETL.Domain.Entities.Api;

namespace VentasETL.WebApi.Api.Data.Context
{
    public class ApiContext : DbContext
    {

        public ApiContext(DbContextOptions<ApiContext> opt) : base(opt) { }


        public DbSet<ProductUpdate> ProductsUpdated { get; set; }
        public DbSet<DataCustumerUpdate> CustumerUpdated{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataCustumerUpdate>()
                .HasNoKey() 
                .ToView("DataCustumerUpdate", "dbo");


            modelBuilder.Entity<ProductUpdate>()
           .HasNoKey()
           .ToView("ProductUpdate", "dbo");


            base.OnModelCreating(modelBuilder);
        }





    }
}
