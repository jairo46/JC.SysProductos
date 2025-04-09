using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.EN
{
	public class Cliente
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
		[StringLength(255, ErrorMessage = "El nombre no puede tener más de 255 caracteres.")]
		public string? Nombre { get; set; }
		public string? Telefono { get; set; }
		[StringLength(100, ErrorMessage = "El correo electronico no puede tener más de 100 caracteres.")]
		[EmailAddress(ErrorMessage = "El correo electronico no tiene un formato valido.")]
		public string? Email { get; set; }
	}
}
