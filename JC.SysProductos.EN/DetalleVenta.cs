using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.EN
{
	public class DetalleVenta
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "El producto es obligatorio.")]
		[ForeignKey("Producto")]
		public int IdProducto { get; set; }
		public int IdVenta { get; set; }
		[Required(ErrorMessage = "La cantidad es obligatoria. ")]
		[Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
		public int Cantidad { get; set; }
		[Required(ErrorMessage = "El precio unitario es obligatorio. ")]
		[Column(TypeName = "decimal(10,2)")]
		public decimal PrecioUnitario { get; set; }
		[Required(ErrorMessage = "El subtotal es obligatorio. ")]
		[Range(0.01, 99999999.99, ErrorMessage = "El subtotal debe ser mayor a 0.")]
		[Column(TypeName = "decimal(10,2)")]
		public decimal SubTotal { get; set; }

		// Relación con Venta (Cada detalle pertenece a una venta)
		public virtual Venta? Venta { get; set; }

		// Relación con Producto (Cada detalle está asociado a un producto)
		public virtual Productos? Producto { get; set; }
	}
}
