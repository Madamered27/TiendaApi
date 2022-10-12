using System.Data;
using System.Data.SqlClient;
using TiendaApi.Conexion;
using TiendaApi.Modelo;
namespace TiendaApi.Datos;

public class Dproductos
{
    ConexionBD cn = new ConexionBD();
    public async Task<List<MProductos>> MostrarProductos()
    {
        var lista = new List<MProductos>();

        //Conexion a bd
        using (var sql = new SqlConnection(cn.cadenaSQL()))
        {
            //Ejecucion store procedure
            using (var cmd = new SqlCommand("mostrarProductos", sql))
            {
                //Abrir conexion
                //como se usa tarea asincrona se llama a await
                await sql.OpenAsync();

                //Se indica que es procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;

                //Recorrido de datos
                using (var item = await cmd.ExecuteReaderAsync())
                {
                    while (await item.ReadAsync())
                    {
                        var mProductos = new MProductos();
                        mProductos.Id = (int)item["Id"];
                        mProductos.Descripcion = (string)item["descripcion"];
                        mProductos.Precio = (decimal)item["precio"];
                        lista.Add(mProductos);
                    }
                }
            }
        }
        return lista;
    }

    public async Task InsertarProducto(MProductos producto)
    {
        using (var sql = new SqlConnection(cn.cadenaSQL()))
        {
            using (var cmd = new SqlCommand("insertarProductos", sql))
            {
                await sql.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;

                //Se indica que tiene parametros
                cmd.Parameters.AddWithValue("descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("precio", producto.Precio);

                await cmd.ExecuteNonQueryAsync();

            }
        }
    }

    public async Task EditarProducto(MProductos producto)
    {
        using (var sql = new SqlConnection(cn.cadenaSQL()))
        {
            using (var cmd = new SqlCommand("editarProductos", sql))
            {
                await sql.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;

                //Se indica que tiene parametros
                cmd.Parameters.AddWithValue("id", producto.Id);
                cmd.Parameters.AddWithValue("precio", producto.Precio);

                await cmd.ExecuteNonQueryAsync();

            }
        }
    }

    public async Task EliminarProducto(MProductos producto)
    {
        using (var sql = new SqlConnection(cn.cadenaSQL()))
        {
            using (var cmd = new SqlCommand("eliminarProductos", sql))
            {
                await sql.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", producto.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }







}



