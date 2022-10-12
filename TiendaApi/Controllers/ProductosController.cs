 using Microsoft.AspNetCore.Mvc;
using TiendaApi.Datos;
using TiendaApi.Modelo;

namespace TiendaApi.Controllers;

[ApiController]
[Route("api/Productos")]
public class ProductosController : Controller
{

    [HttpGet]
    // Tareas asincronas esperan que termine de ejecutar el proceso 1 para pasar al 2
    public async Task<ActionResult<List<MProductos>>> Get()
    {
        var funcion = new Dproductos();
        var lista = await funcion.MostrarProductos();

        return lista;
    }

    [HttpPost]
    public async Task Post([FromBody] MProductos producto)
    {
        var funcion = new Dproductos();
        await funcion.InsertarProducto(producto);

    }

    [HttpPut("{id}")]
    public async Task <ActionResult> Put(int id, [FromBody] MProductos producto)
    {
        var funcion = new Dproductos();
        producto.Id = id;
        await funcion.EditarProducto(producto);
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var funcion = new Dproductos();
        var producto = new MProductos();
        producto.Id=id;
        await funcion.EliminarProducto(producto);
        return NoContent();
    }

}
