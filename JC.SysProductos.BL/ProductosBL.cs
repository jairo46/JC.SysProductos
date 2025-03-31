using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JC.SysProductos.DAL;
using JC.SysProductos.EN;

namespace JC.SysProductos.BL
{
    public class ProductosBL
    {
        // Declarar campo privado y de lectura para alnacanar la dependancia de la capa de acceso a datos (DAL)
        private readonly ProductosDAL _ProductosDAl;

        // Constructor de la clase Productoset que recibe una instancia de ProductosDAL, como parimetre.
        public ProductosBL(ProductosDAL productosDAL)
        {
            // Asigna la instancia recibida a la variable de sule lecturs ProductosDAL
            // Este permite que la capa de lógica de negocios utilice los métodos de acceso a dates.
            _ProductosDAl = productosDAL;
        }
        public async Task<int> CrearAsync(Productos pProductos)
        {
            return await _ProductosDAl.CrearAsync(pProductos);
        }
        public async Task<int> ModificarAsync(Productos pProductos)
        {
            return await _ProductosDAl.ModificarAsync(pProductos);
        }
        public async Task<int> EliminarAsync(Productos pProductos)
        {
            return await _ProductosDAl.EliminarAsync(pProductos);
        }
        public async Task<Productos> ObtenerPorIdAsync(Productos pProductos)
        {
            return await _ProductosDAl.ObtenerPorIdAsync(pProductos);
        }
        public async Task<List<Productos>> ObtenerTodosAsync()
        {
            return await _ProductosDAl.ObtenerTodosAsync();
        }
        public Task AgregarTodosAsync(List<Productos> pProductos)
        {
            return _ProductosDAl.AgregarTodosAsync(pProductos);
        }
    }
}
