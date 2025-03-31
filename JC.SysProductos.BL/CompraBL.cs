﻿using JC.SysProductos.DAL;
using JC.SysProductos.EN;
using JC.SysProductos.EN.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JC.SysProductos.BL
{
	public class CompraBL
	{
		readonly CompraDAL compraDAL;
		public CompraBL(CompraDAL pCompraDAL)
		{
			compraDAL = pCompraDAL;
		}
		public async Task<int> CrearAsync(Compra pCompra)
		{
			return await compraDAL.CrearAsync(pCompra);
		}
		public async Task<int> AnularAsync(int idCompra)
		{
			return await compraDAL.AnularAsync(idCompra);
		}
		public async Task<Compra> ObtenerPorIdAsync(int idCompra)
		{
			return await compraDAL.ObtenerPorIdAsync(idCompra);
		}
		public async Task<List<Compra>> ObtenerTodosAsync()
		{
			return await compraDAL.ObtenerTodosAsync();
		}
		public async Task<List<Compra>> ObtenerPorEstadoAsync(byte estado)
		{
			return await compraDAL.ObtenerPorEstadoAsync(estado);
		}
        public async Task<List<Compra>> ObtenerReporteComprasAsync(CompraFiltros filtro)
        {
            return await compraDAL.ObtenerReporteComprasAsync(filtro);
        }
    }
}
