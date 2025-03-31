using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.EN
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre no puede tener  mas de 255 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El MRC no puede tener  mas de 50 caracteres.")]
        public string? NRC { get; set;}

        [StringLength(80, ErrorMessage = "La dirección no puede tener  mas de 80 caracteres.")]
        public string? Direccion { get; set; }

        [StringLength(20, ErrorMessage = "El Telefono no puede tener  mas de 20 caracteres.")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "El correo electronico no puede tener  mas de 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El correo electronico no tiene un formato valido.")]

        public string? Email { get; set; }

    }
}
