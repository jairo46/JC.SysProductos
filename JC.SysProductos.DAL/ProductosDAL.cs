using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JC.SysProductos.EN;
namespace JC.SysProductos.DAL
{
    public class ProductosDAL
    {
        readonly SysProductosDBContext _dbContext;
        public ProductosDAL(SysProductosDBContext sysProductosDBContext)
        {
            _dbContext = sysProductosDBContext;
        }
        public async Task<int> CrearAsync(Productos pProductos)
        {
            Productos productos = new Productos()
            {
                Id = pProductos.Id,
                Nombre = pProductos.Nombre,
                Precio = pProductos.Precio,
                CantidadDisponible = pProductos.CantidadDisponible,
                FechaCreacion = pProductos.FechaCreacion,


            };

            _dbContext.Add(productos);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> EliminarAsync(Productos pProductos)
        {
            var productos = _dbContext.Productos.FirstOrDefault(s => s.Id == pProductos.Id);
            if (productos != null)
            {
                _dbContext.Productos.Remove(productos);
                return await _dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> ModificarAsync(Productos pProductos)
        {
            var productos = await _dbContext.Productos.FirstOrDefaultAsync(s => s.Id == pProductos.Id);
            if (productos != null && productos.Id > 0)
            {
                productos.Nombre = pProductos.Nombre;
                productos.Precio = pProductos.Precio;
                productos.CantidadDisponible = pProductos.CantidadDisponible;
                productos.FechaCreacion = pProductos.FechaCreacion;

                _dbContext.Productos.Update(productos);
                return await _dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }
        public async Task<Productos> ObtenerPorIdAsync(Productos pProductos)
        {
            var productos = await _dbContext.Productos.FirstOrDefaultAsync(s => s.Id == pProductos.Id);
            if (productos != null && productos.Id != 0)
            {
                return new Productos
                {
                    Id = productos.Id,
                    Nombre = productos.Nombre,
                    Precio = productos.Precio,
                    CantidadDisponible = productos.CantidadDisponible,
                    FechaCreacion = productos.FechaCreacion
                };
            }
            else
                return new Productos();

        }
        public async Task<List<Productos>> ObtenerTodosAsync()
        {
            var productos = await _dbContext.Productos.ToListAsync();
            if (productos != null && productos.Count > 0)
            {
                var list = new List<Productos>();
                productos.ForEach(s => list.Add(new Productos
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    CantidadDisponible = s.CantidadDisponible,
                    FechaCreacion = s.FechaCreacion
                }));
                return list;
            }
            else
                return new List<Productos>();
        }
        public async Task AgregarTodosAsync(List<Productos> pProductos)
        {
            await _dbContext.Productos.AddRangeAsync(pProductos);
            await _dbContext.SaveChangesAsync();
        }
    }
}
