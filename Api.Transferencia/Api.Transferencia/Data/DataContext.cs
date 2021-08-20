using Api.Transferencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Transferencia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
          : base(options) { }

        public DbSet<TransferInput> TransferInputs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            //builder.Entity<TransferInput>().HasKey(m => m.Id);
            //builder.Entity<TransferInput>().HasOne(m => m.AccountOrigin).WithMany().HasForeignKey(u => u.AccountOrigin);

            base.OnModelCreating(builder);
        }
    }
}