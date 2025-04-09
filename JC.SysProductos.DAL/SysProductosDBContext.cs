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
		public DbSet<Venta> Ventas { get; set; }
		public DbSet<DetalleVenta> DetalleVentas { get; set; }
		public DbSet<Cliente> Clientes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		 modelBuilder.Entity<DetalleCompra>()
             .HasOne(d => d.Compra)
             .WithMany(c => c.DetalleCompras)
             .HasForeignKey(d => d.IdCompra);
		 
		 modelBuilder.Entity<DetalleVenta>()
             .HasOne(d => d.Venta)
             .WithMany(v => v.DetalleVentas)
             .HasForeignKey(d => d.IdVenta);

		 base.OnModelCreating(modelBuilder);
		}
    }
}