using JC.SysProductos.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.DAL
{
	public class ClienteDAL
	{
		readonly SysProductosDBContext dbContext;
		public ClienteDAL(SysProductosDBContext sysProductosDB)
		{
			dbContext = sysProductosDB;
		}
		public async Task<int> CrearAsync(Cliente pCliente)
		{
			Cliente cliente = new Cliente()
			{
				Nombre = pCliente.Nombre,
				Telefono = pCliente.Telefono,
				Email = pCliente.Email
			};
			dbContext.Clientes.Add(cliente);
			return await dbContext.SaveChangesAsync();
		}
		public async Task<int> EliminarAsync(Cliente pCliente)
		{
			var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
			if (cliente != null && cliente.Id != 0)
			{
				dbContext.Clientes.Remove(cliente);
				return await dbContext.SaveChangesAsync();
			}
			else
				return 0;
		}
		public async Task<int> ModificarAsync(Cliente pCliente)
		{
			var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
			if (cliente != null && cliente.Id != 0)
			{
				cliente.Nombre = pCliente.Nombre;
				cliente.Telefono = pCliente.Telefono;
				cliente.Email = pCliente.Email;

				dbContext.Update(cliente);
				return await dbContext.SaveChangesAsync();
			}
			else
				return 0;
		}
		public async Task<Cliente> ObtenerPorIdAsync(Cliente pCliente)
		{
			var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
			if (cliente != null && cliente.Id != 0)
			{
				return new Cliente
				{
					Id = cliente.Id,
					Nombre = cliente.Nombre,
					Telefono = cliente.Telefono,
					Email = pCliente.Email
				};
			}
			else
				return new Cliente();
		}
		public async Task<List<Cliente>> ObtenerTodosAsync()
		{
			var clientes = await dbContext.Clientes.ToListAsync();
			if (clientes != null && clientes.Count > 0)
			{
				var list = new List<Cliente>();
				clientes.ForEach(p => list.Add(new Cliente
				{
					Id = p.Id,
					Nombre = p.Nombre,
					Telefono = p.Telefono,
					Email = p.Email
				}));
				return list;
			}
			else
				return new List<Cliente>();

		}
		public async Task AgregarTodosAsync(List<Cliente> pCliente)
		{
			await dbContext.Clientes.AddRangeAsync(pCliente);
			await dbContext.SaveChangesAsync();
		}
	}
}
