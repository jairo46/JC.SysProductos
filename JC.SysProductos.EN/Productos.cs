using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.EN
{
    public class Productos
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El password es obligatorio")]
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }
        public DateTime FechaCreacion { get; set; } //Relación de uno a muchos
    }
}
