using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JC.SysProductos.BL;
using JC.SysProductos.EN;
using Rotativa.AspNetCore;
using OfficeOpenXml;

namespace JC.SysProductos.AppWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductosBL _productosBL; //definir campo de lectura para almacenar la dependencia de la capa de BL de PRODUCTO
        public ProductosController(ProductosBL pProductosBl)
        {
            _productosBL = pProductosBl; // permite que la  capa web utilice los metodos de la capa BL.
        }
        // GET: ProductosController
        public async Task<ActionResult> Index()
        {
            var productos = await _productosBL.ObtenerTodosAsync(); //obtener los productos de la tabla
            return View(productos); //pasar la lista de productos a la vista
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Productos pProductos)
        {
            try
            {
                await _productosBL.CrearAsync(pProductos);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var productos = await _productosBL.ObtenerPorIdAsync(new Productos { Id = id });
            return View(productos);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Productos pProductos)
        {
            try
            {
                await _productosBL.ModificarAsync(pProductos);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var productos = await _productosBL.ObtenerPorIdAsync(new Productos { Id = id });
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Productos pProductos)
        {
            try
            {
                await _productosBL.EliminarAsync(new Productos { Id = pProductos.Id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> ReporteProductos()
        {
            var productos = await _productosBL.ObtenerTodosAsync();
            return new ViewAsPdf("rpProductos", productos);
        }
        public async Task<JsonResult> ProductosJson()
        {
            var productos = await _productosBL.ObtenerTodosAsync();

            var productosData = productos
                .Select(p => new
                {
                    nombre = p.Nombre,
                    stock = p.CantidadDisponible,
                })
                .ToList();

            return Json(productosData);
        }
        public async Task<JsonResult> ProductosJsonPrecio()
        {
            var productos = await _productosBL.ObtenerTodosAsync();

            var productosData = productos
                .Select(p => new
                {
                    fechaCreacion = p.FechaCreacion.ToString("yyyy-MM-dd"),
                    precio = p.Precio
                })
                .ToList();

            var groupedData = productosData
                .GroupBy(p => p.fechaCreacion)
                .Select(g => new
                {
                    fecha = g.Key,
                    precioPromedio = g.Average(p => p.precio) //Calcular el precio promedio
                })
                .OrderBy(g => g.fecha)
                .ToList();

            return Json(groupedData);
        }
        public async Task<IActionResult> ReporteProductosExcel()
        {
            var productos = await _productosBL.ObtenerTodosAsync();
            using (var package = new ExcelPackage())
            {
                var hojaExcel = package.Workbook.Worksheets.Add("Productos");

                hojaExcel.Cells["A1"].Value = "Nombre";
                hojaExcel.Cells["B1"].Value = "Precio";
                hojaExcel.Cells["C1"].Value = "Cantidad";
                hojaExcel.Cells["D1"].Value = "Fecha";

                int row = 2;
                foreach (var producto in productos)
                {
                    hojaExcel.Cells[row, 1].Value = producto.Nombre;
                    hojaExcel.Cells[row, 2].Value = producto.Precio;
                    hojaExcel.Cells[row, 3].Value = producto.CantidadDisponible;
                    hojaExcel.Cells[row, 4].Value = producto.FechaCreacion.ToString("yyyy-MM-dd");
                    row++;
                }
                hojaExcel.Cells["A:D"].AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformarts-officedocument.spreadsheet.sheet", "ReporteProductosExcel.xlsx");
            }


        }
        public async Task<IActionResult> SubirExcelProductos(IFormFile archivoExcel)
        {
            if (archivoExcel == null || archivoExcel.Length == 0)
            {
                return RedirectToAction("Index");
            }

            var productos = new List<Productos>();

            using (var stream = new MemoryStream())
            {
                await archivoExcel.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream)) 
                {
                    var hojaExcel = package.Workbook.Worksheets[0];

                    int rowCount = hojaExcel.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var nombre = hojaExcel.Cells[row, 1].Text;
                        var precio = hojaExcel.Cells[row, 2].GetValue<decimal>();
                        var cantidad = hojaExcel.Cells[row, 3].GetValue<int>();
                        var fecha = hojaExcel.Cells[row, 4].GetValue<DateTime>();

                        if (string.IsNullOrEmpty(nombre) || precio <= 0 || cantidad <= 0)
                            continue;
                        productos.Add(new Productos
                        {
                            Nombre = nombre,
                            Precio = precio,
                            CantidadDisponible = cantidad,
                            FechaCreacion = fecha,
                        });
                    }
                }
                if (productos.Count > 0)
                {
                    await _productosBL.AgregarTodosAsync(productos);
                }
                return RedirectToAction("Index");
            }

        }
    }
}
