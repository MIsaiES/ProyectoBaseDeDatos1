using ProyectoBaseDeDatos1.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TiposMovimientoDAO
{
    private readonly string _connectionString = ConexionSQL.cadenaConexion;


    public void Create(TiposMovimiento tipo)
    {
        const string sql = @"
                INSERT INTO Tb_TiposMovimiento (CodigoTipo, NombreTipo, Descripcion, AfectaStock, SignoMovimiento)
                VALUES (@CodigoTipo, @NombreTipo, @Descripcion, @AfectaStock, @SignoMovimiento);
                SELECT SCOPE_IDENTITY();";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@CodigoTipo", tipo.CodigoTipo);
            command.Parameters.AddWithValue("@NombreTipo", tipo.NombreTipo);
            command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@AfectaStock", tipo.AfectaStock);
            command.Parameters.AddWithValue("@SignoMovimiento", tipo.SignoMovimiento);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public string GetIdBySign(int id)
    {
        const string sql = "SELECT SignoMovimiento FROM Tb_TiposMovimiento WHERE ID_TipoMovimiento = @ID";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {  
                    string signo = reader.GetString(reader.GetOrdinal("SignoMovimiento"));
                    connection.Close();
                    return signo;
                    
                }
            }
            connection.Close();
        }
        return null;
    }

    public List<TiposMovimiento> GetAll()
    {
        const string sql = "SELECT * FROM Tb_TiposMovimiento";
        var tipos = new List<TiposMovimiento>();
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tipos.Add(MapReader(reader));
                }
            }
            connection.Close();
        }
        return tipos;
    }

    public void Update(TiposMovimiento tipo)
    {
        const string sql = @"
                UPDATE Tb_TiposMovimiento 
                SET CodigoTipo = @CodigoTipo, NombreTipo = @NombreTipo, Descripcion = @Descripcion,
                    AfectaStock = @AfectaStock, SignoMovimiento = @SignoMovimiento
                WHERE ID_TipoMovimiento = @ID";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", tipo.ID_TipoMovimiento);
            command.Parameters.AddWithValue("@CodigoTipo", tipo.CodigoTipo);
            command.Parameters.AddWithValue("@NombreTipo", tipo.NombreTipo);
            command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@AfectaStock", tipo.AfectaStock);
            command.Parameters.AddWithValue("@SignoMovimiento", tipo.SignoMovimiento);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Tb_TiposMovimiento WHERE ID_TipoMovimiento = @ID";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();;
        }
    }

    private static TiposMovimiento MapReader(SqlDataReader reader)
    {
        return new TiposMovimiento
        {
            ID_TipoMovimiento = reader.GetInt32(reader.GetOrdinal("ID_TipoMovimiento")),
            CodigoTipo = reader.GetString(reader.GetOrdinal("CodigoTipo")),
            NombreTipo = reader.GetString(reader.GetOrdinal("NombreTipo")),
            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
            AfectaStock = reader.GetBoolean(reader.GetOrdinal("AfectaStock")),
            SignoMovimiento = reader.GetString(reader.GetOrdinal("SignoMovimiento"))
        };
    }
}