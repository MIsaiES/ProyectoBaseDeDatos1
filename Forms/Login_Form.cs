using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoBaseDeDatos1.Datos;


namespace Proyecto_Zoologico.Formularios
{
    public partial class Login_Form : Form
    {
        // Información del usuario logueado
        public static string UsuarioActual { get; private set; }
        public static string RolActual { get; private set; }
        public static int UsuarioId { get; private set; }
        public static int RolId { get; private set; }

        public Login_Form()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar Enter key para navegar
            this.txtUsuario.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    txtPassword.Focus();
                    e.Handled = true;
                }
            };

            this.txtPassword.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    btnIniciarSesion_Click(null, null);
                    e.Handled = true;
                }
            };
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Por favor ingrese su nombre de usuario", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Por favor ingrese su contraseña", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Deshabilitar botón para evitar doble clic
                btnIniciarSesion.Enabled = false;
                btnIniciarSesion.Text = "Validando...";
                Application.DoEvents();

                // Intentar autenticación
                if (AutenticarUsuario(txtUsuario.Text.Trim(), txtPassword.Text))
                {
                    MessageBox.Show($"¡Bienvenido {UsuarioActual}!\nRol: {RolActual}", 
                        "Inicio de Sesión Exitoso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    var form = new Inventario_Form();
                    form.ShowDialog();
                    return;
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.\n\nPor favor verifique sus credenciales.",
                        "Error de Autenticación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnIniciarSesion.Enabled = true;
                btnIniciarSesion.Text = "INICIAR SESIÓN";
            }
        }

        private bool AutenticarUsuario(string usuario, string password)
        {
            try
            {
                // Construir cadena de conexión con credenciales del usuario
                string connectionString = $"Server=ISAIESQUIVEL;Database=db_SistemaInventario;" +
                    $"User Id={usuario};Password={password};";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consultar información del usuario
                    string query = @"
                        SELECT 
                            u.ID_Usuarios,
                            u.NombreUsuario,
                            u.EmailUsuario,
                            u.ID_Rol,
                            r.NombreRol,
                            r.NivelAcceso
                        FROM Tb_Usuarios u
                        INNER JOIN Tb_Roles r ON u.ID_Rol = r.ID_Rol
                        WHERE u.NombreUsuario = @Usuario 
                        AND u.EstadoActivo = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Guardar información del usuario
                                UsuarioId = reader.GetInt32(0);
                                UsuarioActual = reader.GetString(1);
                                RolId = reader.GetInt32(3);
                                RolActual = reader.GetString(4);

                                // Actualizar cadena de conexión global con credenciales validadas
                                ConexionSQL.cadenaConexion = connectionString;

                                return true;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Error de autenticación SQL (usuario/contraseña incorrectos)
                if (sqlEx.Number == 18456)
                {
                    return false;
                }
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la autenticación: {ex.Message}", ex);
            }

            return false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea salir?",
                "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkMostrarPassword.Checked ? '\0' : '●';
        }

        // Método estático para cerrar sesión
        public static void CerrarSesion()
        {
            UsuarioActual = null;
            RolActual = null;
            UsuarioId = 0;
            RolId = 0;
            ProyectoBaseDeDatos1.Datos.ConexionSQL.cadenaConexion = string.Empty;
        }

        // Método para verificar permisos por nivel de acceso
        public static bool TienePermiso(int nivelRequerido)
        {
            // Los roles con mayor nivel de acceso tienen todos los permisos
            // ADMINISTRADOR_SISTEMA: 10
            // GERENTE_INVENTARIO: 8
            // BODEGUERO: 5
            // CONSULTOR: 3
            // AUDITOR: 2
            
            // Aquí deberías consultar el nivel de acceso del usuario actual
            // Por ahora retornamos true para desarrollo
            return true;
        }
    }
}