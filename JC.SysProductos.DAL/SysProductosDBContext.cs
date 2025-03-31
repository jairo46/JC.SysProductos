using JC.SysProductos.EN;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace JC.SysProductos.DAL
{
    public class SysProductosDBContext : DbContext
    {
        public SysProductosDBContext(DbContextOptions<SysProductosDBContext> options) : base(options)
        {

        }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		 modelBuilder.Entity<DetalleCompra>()
             .HasOne(d => d.Compra)
             .WithMany(c => c.DetalleCompras)
             .HasForeignKey(d => d.IdCompra);
		 base.OnModelCreating(modelBuilder);
		}
    }
}