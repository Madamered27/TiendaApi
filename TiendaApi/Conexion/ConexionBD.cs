namespace TiendaApi.Conexion;

public class ConexionBD
{
    private string conectionBD = string.Empty;


    public ConexionBD()
    {
        var constructor = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
        conectionBD = constructor.GetSection("ConnectionStrings:conexion").Value;
    }

    public string cadenaSQL()
    {
        return conectionBD;
    }



}
