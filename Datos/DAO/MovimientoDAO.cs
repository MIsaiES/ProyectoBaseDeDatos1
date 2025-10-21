using ProyectoBaseDeDatos1.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MovimientosDAO
{
    private readonly string _connectionString = ConexionSQL.cadenaConexion;

    public void Create(Movimientos movimiento)
    {
        const string sql = @"
                INSERT INTO Tb_Movimientos (ID_Productos, ID_Bodegas, ID_TipoMovimiento, ID_Usuarios, Cantidad, FechaMovimiento, Comentario, ReferenciaExterna)
                VALUES (@ID_Productos, @ID_Bodegas, @ID_TipoMovimiento, @ID_Usuarios, @Cantidad, @FechaMovimiento, @Comentario, @ReferenciaExterna);
                SELECT SCOPE_IDENTITY();";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID_Productos", movimiento.ID_Productos);
            command.Parameters.AddWithValue("@ID_Bodegas", movimiento.ID_Bodegas);
            command.Parameters.AddWithValue("@ID_TipoMovimiento", movimiento.ID_TipoMovimiento);
            command.Parameters.AddWithValue("@ID_Usuarios", movimiento.ID_Usuarios);
            command.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
            command.Parameters.AddWithValue("@FechaMovimiento", movimiento.FechaMovimiento);
            command.Parameters.AddWithValue("@Comentario", movimiento.Comentario ?? string.Empty);
            command.Parameters.AddWithValue("@ReferenciaExterna", movimiento.ReferenciaExterna ?? string.Empty);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public Movimientos GetById(int id)
    {
        const string sql = "SELECT * FROM Tb_Movimientos WHERE ID_Movimientos = @ID";
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

    public List<Movimientos> GetAll()
    {
        const string sql = "SELECT * FROM Tb_Movimientos";
        var movimientos = new List<Movimientos>();
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    movimientos.Add(MapReader(reader));
                }
            }
            connection.Close();
        }

        return movimientos;
    }

    public void Update(Movimientos movimiento)
    {
        const string sql = @"
                UPDATE Tb_Movimientos 
                SET ID_Productos = @ID_Productos, ID_Bodegas = @ID_Bodegas, ID_TipoMovimiento = @ID_TipoMovimiento,
                    ID_Usuarios = @ID_Usuarios, Cantidad = @Cantidad, FechaMovimiento = @FechaMovimiento,
                    Comentario = @Comentario, ReferenciaExterna = @ReferenciaExterna
                WHERE ID_Movimientos = @ID";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", movimiento.ID_Movimientos);
            command.Parameters.AddWithValue("@ID_Productos", movimiento.ID_Productos);
            command.Parameters.AddWithValue("@ID_Bodegas", movimiento.ID_Bodegas);
            command.Parameters.AddWithValue("@ID_TipoMovimiento", movimiento.ID_TipoMovimiento);
            command.Parameters.AddWithValue("@ID_Usuarios", movimiento.ID_Usuarios);
            command.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
            command.Parameters.AddWithValue("@FechaMovimiento", movimiento.FechaMovimiento);
            command.Parameters.AddWithValue("@Comentario", movimiento.Comentario ?? string.Empty);
            command.Parameters.AddWithValue("@ReferenciaExterna", movimiento.ReferenciaExterna ?? string.Empty);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Tb_Movimientos WHERE ID_Movimientos = @ID";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public List<Movimientos> ObtenerMovimientos()
    {
        string query = @"
            use db_SistemaInventario
            SELECT 
                m.ID_Movimientos,
                m.ID_Productos,
                p.Prod_Nombre AS Producto,
                m.ID_Bodegas,
                b.Bdg_Nombre AS Bodega,
                m.ID_TipoMovimiento,
                tm.NombreTipo AS TipoMovimiento,
                m.ID_Usuarios,
                u.NombreUsuario AS Usuario,
                m.Cantidad,
                m.FechaMovimiento,
                m.Comentario,
                m.ReferenciaExterna
            FROM Tb_Movimientos  AS m
            INNER JOIN Tb_Productos p ON m.ID_Productos = p.ID_Productos
            INNER JOIN Tb_Bodegas b ON m.ID_Bodegas = b.ID_Bodegas
            INNER JOIN Tb_TiposMovimiento tm ON m.ID_TipoMovimiento = tm.ID_TipoMovimiento
            INNER JOIN Tb_Usuarios u ON m.ID_Usuarios = u.ID_Usuarios
            ORDER BY m.FechaMovimiento DESC";

        List<Movimientos> movimientos = new List<Movimientos>();
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(query, connection))
        {
            connection.Open();
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movimientos.Add(MapReader(reader));
                    }
                }
            }
            return movimientos;
        }
    }

    public DataTable BuscarMovimientos(string campo, string valor)
    {
        string query = $@"
             use db_SistemaInventario
                SELECT 
                 m.ID_Movimientos,
                 m.ID_Productos,
                 p.Prod_Nombre AS Producto,
                 m.ID_Bodegas,
                 b.Bdg_Nombre AS Bodega,
                 m.ID_TipoMovimiento,
                 tm.NombreTipo AS TipoMovimiento,
                 m.ID_Usuarios,
                 u.NombreUsuario AS Usuario,
                 m.Cantidad,
                 m.FechaMovimiento,
                 m.Comentario,
                 m.ReferenciaExterna
            FROM Tb_Movimientos  AS m
            INNER JOIN Tb_Productos p ON m.ID_Productos = p.ID_Productos
            INNER JOIN Tb_Bodegas b ON m.ID_Bodegas = b.ID_Bodegas
            INNER JOIN Tb_TiposMovimiento tm ON m.ID_TipoMovimiento = tm.ID_TipoMovimiento
            INNER JOIN Tb_Usuarios u ON m.ID_Usuarios = u.ID_Usuarios
            WHERE m.{campo} = @Valor";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@Valor", valor);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerProductos()
    {
        string query = "SELECT ID_Productos, Prod_Nombre FROM Tb_Productos ORDER BY Prod_Nombre";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerBodegas()
    {
        string query = "SELECT ID_Bodegas, Bdg_Nombre FROM Tb_Bodegas ORDER BY Bdg_Nombre";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerTiposMovimiento()
    {
        string query = "SELECT ID_TipoMovimiento, NombreTipo FROM Tb_TiposMovimiento ORDER BY NombreTipo";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerUsuarios()
    {
        string query = "SELECT ID_Usuarios, NombreUsuario FROM Tb_Usuarios ORDER BY NombreUsuario";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    private static Movimientos MapReader(SqlDataReader reader)
    {
        return new Movimientos
        {
            ID_Movimientos = reader.GetInt32(reader.GetOrdinal("ID_Movimientos")),
            ID_Productos = reader.GetInt32(reader.GetOrdinal("ID_Productos")),
            ID_Bodegas = reader.GetInt32(reader.GetOrdinal("ID_Bodegas")),
            ID_TipoMovimiento = reader.GetInt32(reader.GetOrdinal("ID_TipoMovimiento")),
            ID_Usuarios = reader.GetInt32(reader.GetOrdinal("ID_Usuarios")),
            Cantidad = reader.GetDecimal(reader.GetOrdinal("Cantidad")),
            FechaMovimiento = reader.GetDateTime(reader.GetOrdinal("FechaMovimiento")),
            Comentario = reader.IsDBNull(reader.GetOrdinal("Comentario")) ? string.Empty : reader.GetString(reader.GetOrdinal("Comentario")),
            ReferenciaExterna = reader.IsDBNull(reader.GetOrdinal("ReferenciaExterna")) ? string.Empty : reader.GetString(reader.GetOrdinal("ReferenciaExterna"))
        };
    }
}