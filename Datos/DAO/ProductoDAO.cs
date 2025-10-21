using Proyecto_Zoologico.Formularios;
using ProyectoBaseDeDatos1.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProductosDAO
{
    private readonly string _connectionString = ConexionSQL.cadenaConexion;

    public void Create(Productos producto)
    {
        const string sql = @"
                INSERT INTO Tb_Productos (ID_TipoProducto, ID_Marca, ID_Proveedor, ID_ProductoEstado, ID_UniMed, ID_UsuarioCreador,
                                          Prod_SKU, Prod_Nombre, Prod_Descripcion, Prod_CostoUnitario, Prod_PorcentajeDescuentod,
                                          Prod_RutaImagen, Prod_FechaCreacion, Prod_FechaModificacion)
                VALUES (@ID_TipoProducto, @ID_Marca, @ID_Proveedor, @ID_ProductoEstado, @ID_UniMed, @ID_UsuarioCreador,
                        @Prod_SKU, @Prod_Nombre, @Prod_Descripcion, @Prod_CostoUnitario, @Prod_PorcentajeDescuentod,
                        @Prod_RutaImagen, @Prod_FechaCreacion, @Prod_FechaModificacion);
                SELECT SCOPE_IDENTITY();";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID_TipoProducto", producto.ID_TipoProducto);
            command.Parameters.AddWithValue("@ID_Marca", producto.ID_Marca);
            command.Parameters.AddWithValue("@ID_Proveedor", producto.ID_Proveedor);
            command.Parameters.AddWithValue("@ID_ProductoEstado", producto.ID_ProductoEstado);
            command.Parameters.AddWithValue("@ID_UniMed", producto.ID_UniMed);
            command.Parameters.AddWithValue("@ID_UsuarioCreador", producto.ID_UsuarioCreador);
            command.Parameters.AddWithValue("@Prod_SKU", producto.Prod_SKU);
            command.Parameters.AddWithValue("@Prod_Nombre", producto.Prod_Nombre);
            command.Parameters.AddWithValue("@Prod_Descripcion", producto.Prod_Descripcion);
            command.Parameters.AddWithValue("@Prod_CostoUnitario", producto.Prod_CostoUnitario);

            if (producto.Prod_PorcentajeDescuentod.HasValue)
                command.Parameters.AddWithValue("@Prod_PorcentajeDescuentod", producto.Prod_PorcentajeDescuentod.Value);
            else
                command.Parameters.AddWithValue("@Prod_PorcentajeDescuentod", DBNull.Value);

            command.Parameters.AddWithValue("@Prod_RutaImagen", producto.Prod_RutaImagen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Prod_FechaCreacion", producto.Prod_FechaCreacion);
            command.Parameters.AddWithValue("@Prod_FechaModificacion", producto.Prod_FechaModificacion ?? (object)DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public Productos GetById(int id)
    {
        const string sql = "SELECT * FROM Tb_Productos WHERE ID_Productos = @ID";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {   
                    return MapReader(reader);
                }
                connection.Close();
            }
            connection.Close();
        }
        return null;
    }

    public List<Productos> GetAll()
    {
        const string sql = "SELECT * FROM Tb_Productos";
        var productos = new List<Productos>();
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    productos.Add(MapReader(reader));
                }
            }
            connection.Close();
        }
        return productos;
    }

    public void Update(Productos producto)
    {
        const string sql = @"
                UPDATE Tb_Productos 
                SET ID_TipoProducto = @ID_TipoProducto, ID_Marca = @ID_Marca, ID_Proveedor = @ID_Proveedor,
                    ID_ProductoEstado = @ID_ProductoEstado, ID_UniMed = @ID_UniMed, ID_UsuarioCreador = @ID_UsuarioCreador,
                    Prod_SKU = @Prod_SKU, Prod_Nombre = @Prod_Nombre, Prod_Descripcion = @Prod_Descripcion,
                    Prod_CostoUnitario = @Prod_CostoUnitario, Prod_PorcentajeDescuentod = @Prod_PorcentajeDescuentod,
                    Prod_RutaImagen = @Prod_RutaImagen, Prod_FechaCreacion = @Prod_FechaCreacion,
                    Prod_FechaModificacion = @Prod_FechaModificacion
                WHERE ID_Productos = @ID";

        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", producto.ID_Productos);
            command.Parameters.AddWithValue("@ID_TipoProducto", producto.ID_TipoProducto);
            command.Parameters.AddWithValue("@ID_Marca", producto.ID_Marca);
            command.Parameters.AddWithValue("@ID_Proveedor", producto.ID_Proveedor);
            command.Parameters.AddWithValue("@ID_ProductoEstado", producto.ID_ProductoEstado);
            command.Parameters.AddWithValue("@ID_UniMed", producto.ID_UniMed);
            command.Parameters.AddWithValue("@ID_UsuarioCreador", producto.ID_UsuarioCreador);
            command.Parameters.AddWithValue("@Prod_SKU", producto.Prod_SKU);
            command.Parameters.AddWithValue("@Prod_Nombre", producto.Prod_Nombre);
            command.Parameters.AddWithValue("@Prod_Descripcion", producto.Prod_Descripcion);
            command.Parameters.AddWithValue("@Prod_CostoUnitario", producto.Prod_CostoUnitario);

            if (producto.Prod_PorcentajeDescuentod.HasValue)
                command.Parameters.AddWithValue("@Prod_PorcentajeDescuentod", producto.Prod_PorcentajeDescuentod.Value);
            else
                command.Parameters.AddWithValue("@Prod_PorcentajeDescuentod", DBNull.Value);

            command.Parameters.AddWithValue("@Prod_RutaImagen", producto.Prod_RutaImagen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Prod_FechaCreacion", producto.Prod_FechaCreacion);
            command.Parameters.AddWithValue("@Prod_FechaModificacion", producto.Prod_FechaModificacion ?? (object)DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Tb_Productos WHERE ID_Productos = @ID";
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public DataTable ObtenerProductos()
    {
        string query = @"
            SELECT 
                p.ID_Productos,
                p.Prod_SKU AS SKU,
                p.Prod_Nombre AS Nombre,
                p.Prod_Descripcion AS Descripcion,
                tp.TipoProdNombre AS TipoProducto,
                m.MarcaNombre AS Marca,
                pr.ProvNombre AS Proveedor,
                pe.EstadoProdNombre AS Estado,
                um.NombreUniMed AS UnidadMedida,
                u.NombreUsuario AS UsuarioCreador,
                p.Prod_CostoUnitario AS CostoUnitario,
                p.Prod_PorcentajeDescuentod AS Descuento,
                p.Prod_FechaCreacion AS FechaCreacion,
                p.Prod_FechaModificacion AS FechaModificacion
            FROM Tb_Productos p
            INNER JOIN Tb_ProdTiposProductos tp ON p.ID_TipoProducto = tp.ID_TipoProducto
            INNER JOIN Tb_ProdMarcas m ON p.ID_Marca = m.ID_Marca
            INNER JOIN Tb_ProdProveedores pr ON p.ID_Proveedor = pr.ID_Proveedor
            INNER JOIN Tb_ProdEstados pe ON p.ID_ProductoEstado = pe.ID_ProductoEstado
            INNER JOIN Tb_UnidadesMedida um ON p.ID_UniMed = um.ID_UniMed
            INNER JOIN Tb_Usuarios u ON p.ID_UsuarioCreador = u.ID_Usuarios
            ORDER BY p.Prod_Nombre";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable BuscarProductos(string campo, string valor)
    {
        string query = $@"
            SELECT 
                p.ID_Productos,
                p.Prod_SKU AS SKU,
                p.Prod_Nombre AS Nombre,
                p.Prod_Descripcion AS Descripcion,
                tp.TipoProdNombre AS TipoProducto,
                m.MarcaNombre AS Marca,
                pr.ProvNombre AS Proveedor,
                pe.EstadoProdNombre AS Estado,
                um.NombreUniMed AS UnidadMedida,
                u.NombreUsuario AS UsuarioCreador,
                p.Prod_CostoUnitario AS CostoUnitario,
                p.Prod_PorcentajeDescuentod AS Descuento,
                p.Prod_FechaCreacion AS FechaCreacion,
                p.Prod_FechaModificacion AS FechaModificacion,
                p.Prod_RutaImagen AS Imagen
            FROM Tb_Productos p
            INNER JOIN Tb_ProdTiposProductos tp ON p.ID_TipoProducto = tp.ID_TipoProducto
            INNER JOIN Tb_ProdMarcas m ON p.ID_Marca = m.ID_Marca
            INNER JOIN Tb_ProdProveedores pr ON p.ID_Proveedor = pr.ID_Proveedor
            INNER JOIN Tb_ProdEstados pe ON p.ID_ProductoEstado = pe.ID_ProductoEstado
            INNER JOIN Tb_UnidadesMedida um ON p.ID_UniMed = um.ID_UniMed
            INNER JOIN Tb_Usuarios u ON p.ID_UsuarioCreador = u.ID_Usuarios
            WHERE p.{campo} LIKE @Valor";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@Valor", "%" + valor + "%");
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerTiposProducto()
    {
        string query = "SELECT ID_TipoProducto, TipoProdNombre FROM Tb_ProdTiposProductos ORDER BY TipoProdNombre";
        
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerMarcas()
    {
        string query = "SELECT ID_Marca, MarcaNombre FROM Tb_ProdMarcas ORDER BY MarcaNombre";
        
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerProveedores()
    {
        string query = "SELECT ID_Proveedor, ProvNombre FROM Tb_ProdProveedores ORDER BY ProvNombre";
        
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerEstados()
    {
        string query = "SELECT ID_ProductoEstado, EstadoProdNombre FROM Tb_ProdEstados ORDER BY EstadoProdNombre";
        
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }

    public DataTable ObtenerUnidadesMedida()
    {
        string query = "SELECT ID_UniMed, NombreUniMed FROM Tb_UnidadesMedida ORDER BY NombreUniMed";
        
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

    private static Productos MapReader(SqlDataReader reader)
    {
        return new Productos
        {
            ID_Productos = reader.GetInt32(reader.GetOrdinal("ID_Productos")),
            ID_TipoProducto = reader.GetInt32(reader.GetOrdinal("ID_TipoProducto")),
            ID_Marca = reader.GetInt32(reader.GetOrdinal("ID_Marca")),
            ID_Proveedor = reader.GetInt32(reader.GetOrdinal("ID_Proveedor")),
            ID_ProductoEstado = reader.GetInt32(reader.GetOrdinal("ID_ProductoEstado")),
            ID_UniMed = reader.GetInt32(reader.GetOrdinal("ID_UniMed")),
            ID_UsuarioCreador = reader.GetInt32(reader.GetOrdinal("ID_UsuarioCreador")),
            Prod_SKU = reader.GetString(reader.GetOrdinal("Prod_SKU")),
            Prod_Nombre = reader.GetString(reader.GetOrdinal("Prod_Nombre")),
            Prod_Descripcion = reader.GetString(reader.GetOrdinal("Prod_Descripcion")),
            Prod_CostoUnitario = reader.GetDecimal(reader.GetOrdinal("Prod_CostoUnitario")),
            Prod_PorcentajeDescuentod = reader.IsDBNull(reader.GetOrdinal("Prod_PorcentajeDescuentod")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Prod_PorcentajeDescuentod")),
            Prod_RutaImagen = reader.IsDBNull(reader.GetOrdinal("Prod_RutaImagen")) ? null : (byte[])reader["Prod_RutaImagen"],
            Prod_FechaCreacion = reader.GetDateTime(reader.GetOrdinal("Prod_FechaCreacion")),
            Prod_FechaModificacion = reader.IsDBNull(reader.GetOrdinal("Prod_FechaModificacion")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Prod_FechaModificacion"))
        };
    }
}