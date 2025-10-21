using ProyectoBaseDeDatos1.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InventarioDAO
{
    private readonly string _connectionString = ConexionSQL.cadenaConexion;


    public void Create(Inventario inventario)
    {
        const string sql = @"
                INSERT INTO Tb_Inventario (ID_Productos, ID_Bodegas, CantidadStock, PrecioVenta, StockMinimo, StockMaximo, UltimaActualizacion)
                VALUES (@ID_Productos, @ID_Bodegas, @CantidadStock, @PrecioVenta, @StockMinimo, @StockMaximo, @UltimaActualizacion);
                SELECT SCOPE_IDENTITY();";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID_Productos", inventario.ID_Productos);
            command.Parameters.AddWithValue("@ID_Bodegas", inventario.ID_Bodegas);
            command.Parameters.AddWithValue("@CantidadStock", inventario.CantidadStock);
            command.Parameters.AddWithValue("@PrecioVenta", inventario.PrecioVenta);
            command.Parameters.AddWithValue("@StockMinimo", inventario.StockMinimo);
            command.Parameters.AddWithValue("@StockMaximo", inventario.StockMaximo);
            command.Parameters.AddWithValue("@UltimaActualizacion", inventario.UltimaActualizacion);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }

    public Inventario GetById(int id)
    {
        const string sql = "SELECT * FROM Tb_Inventario WHERE ID_Inventario = @ID"; 
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    connection.Close();
                    return MapReader(reader);
                }
                connection.Close();
            }
        }
        return null;
    }

    public List<Inventario> GetAll()
    {
        const string sql = "SELECT * FROM Tb_Inventario";
        List<Inventario> inventarios = new List<Inventario>();
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    inventarios.Add(MapReader(reader));
                }
            }
        }
        return inventarios;
    }

    public void Update(Inventario inventario)
    {
        const string sql = @"
                UPDATE Tb_Inventario 
                SET ID_Productos = @ID_Productos, ID_Bodegas = @ID_Bodegas, CantidadStock = @CantidadStock,
                    PrecioVenta = @PrecioVenta, StockMinimo = @StockMinimo, StockMaximo = @StockMaximo,
                    UltimaActualizacion = @UltimaActualizacion
                WHERE ID_Inventario = @ID"; 

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", inventario.ID_Inventario);
            command.Parameters.AddWithValue("@ID_Productos", inventario.ID_Productos);
            command.Parameters.AddWithValue("@ID_Bodegas", inventario.ID_Bodegas);
            command.Parameters.AddWithValue("@CantidadStock", inventario.CantidadStock);
            command.Parameters.AddWithValue("@PrecioVenta", inventario.PrecioVenta);
            command.Parameters.AddWithValue("@StockMinimo", inventario.StockMinimo);
            command.Parameters.AddWithValue("@StockMaximo", inventario.StockMaximo);
            command.Parameters.AddWithValue("@UltimaActualizacion", inventario.UltimaActualizacion);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Tb_Inventario WHERE ID_Inventario = @ID"; 
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }

    public List<string> getBodegas()
    {
        List<string> bodegas = new List<string>();
        const string sql = "SELECT * FROM Tb_Bodegas";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bodegas.Add(reader["Bdg_Nombre"].ToString());
                }
            }
            connection.Close();
        }
        return bodegas;
    }

    public int getIdByName(string nameValue)
    {
        int id = -1;
        string sql = $"SELECT ID_Bodegas FROM Tb_Bodegas WHERE Bdg_Nombre = @nameValue";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@nameValue", nameValue);
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["ID_Bodegas"]);
                    }
                }
                connection.Close();
            }
        }
        return id;
    }

    public List<Inventario> getByParameter(string param, string value)
    {
        List<Inventario> inventarios = new List<Inventario>();
        string sql = $"SELECT * FROM Tb_Inventario where {param} = @value";
        var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand(sql, connection);       
        command.Parameters.AddWithValue("@value", value);
        connection.Open();
        var reader = command.ExecuteReader();
                
            while (reader.Read())
            {
                Inventario inventario = new Inventario
                {
                    ID_Inventario = reader.GetInt32(reader.GetOrdinal("ID_Inventario")),
                    ID_Productos = reader.GetInt32(reader.GetOrdinal("ID_Productos")),
                    ID_Bodegas = reader.GetInt32(reader.GetOrdinal("ID_Bodegas")),
                    CantidadStock = reader.GetDecimal(reader.GetOrdinal("CantidadStock")),
                    PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                    StockMinimo = reader.GetDecimal(reader.GetOrdinal("StockMinimo")),
                    StockMaximo = reader.GetDecimal(reader.GetOrdinal("StockMaximo")),
                    UltimaActualizacion = reader.GetDateTime(reader.GetOrdinal("UltimaActualizacion"))
                };
                inventarios.Add(inventario);
        }   
            connection.Close();
            
        
        return inventarios;
    }

    private static Inventario MapReader(SqlDataReader reader)
    {
        return new Inventario
        {
            ID_Inventario = reader.GetInt32(reader.GetOrdinal("ID_Inventario")),
            ID_Productos = reader.GetInt32(reader.GetOrdinal("ID_Productos")),
            ID_Bodegas = reader.GetInt32(reader.GetOrdinal("ID_Bodegas")),
            CantidadStock = reader.GetDecimal(reader.GetOrdinal("CantidadStock")),
            PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
            StockMinimo = reader.GetDecimal(reader.GetOrdinal("StockMinimo")),
            StockMaximo = reader.GetDecimal(reader.GetOrdinal("StockMaximo")),
            UltimaActualizacion = reader.GetDateTime(reader.GetOrdinal("UltimaActualizacion"))
        };
    }
}