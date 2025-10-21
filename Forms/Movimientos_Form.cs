using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBaseDeDatos1.Forms
{
    public partial class Movimientos_Form : Form
    {
        private MovimientosDAO movimientosDAO;
        private InventarioDAO inventarioDAO;
        private int selectedMovimientoId = 0;

        public Movimientos_Form()
        {
            InitializeComponent();
            movimientosDAO = new MovimientosDAO();
            inventarioDAO = new InventarioDAO();
            CargarMovimientos();
            CargarProductos();
            CargarBodegas();
            CargarTiposMovimiento();
            CargarUsuarios();
            dtpFechaMovimiento.Value = DateTime.Now;
        }

        private void CargarMovimientos()
        {
            try
            {
                if (dgvMovimientos.DataSource == null)
                {
                    return;
                }
                List<Movimientos> dt = movimientosDAO.ObtenerMovimientos();
                dgvMovimientos.DataSource = dt;

                // Configurar columnas
                dgvMovimientos.Columns["ID_Movimientos"].HeaderText = "ID";
                dgvMovimientos.Columns["ID_Movimientos"].Width = 50;
                dgvMovimientos.Columns["ID_Productos"].Visible = false;
                dgvMovimientos.Columns["Producto"].HeaderText = "Producto";
                dgvMovimientos.Columns["ID_Bodegas"].Visible = false;
                dgvMovimientos.Columns["Bodega"].HeaderText = "Bodega";
                dgvMovimientos.Columns["ID_TipoMovimiento"].Visible = false;
                dgvMovimientos.Columns["TipoMovimiento"].HeaderText = "Tipo";
                dgvMovimientos.Columns["ID_Usuarios"].Visible = false;
                dgvMovimientos.Columns["Usuario"].HeaderText = "Usuario";
                dgvMovimientos.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvMovimientos.Columns["FechaMovimiento"].HeaderText = "Fecha";
                dgvMovimientos.Columns["FechaMovimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvMovimientos.Columns["Comentario"].HeaderText = "Comentario";
                dgvMovimientos.Columns["ReferenciaExterna"].HeaderText = "Referencia";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar movimientos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                DataTable dt = movimientosDAO.ObtenerProductos();
                cmbProducto.DataSource = dt;
                cmbProducto.DisplayMember = "Prod_Nombre";
                cmbProducto.ValueMember = "ID_Productos";
                cmbProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarBodegas()
        {
            try
            {
                DataTable dt = movimientosDAO.ObtenerBodegas();
                cmbBodega.DataSource = dt;
                cmbBodega.DisplayMember = "Bdg_Nombre";
                cmbBodega.ValueMember = "ID_Bodegas";
                cmbBodega.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bodegas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTiposMovimiento()
        {
            try
            {
                DataTable dt = movimientosDAO.ObtenerTiposMovimiento();
                cmbTipoMovimiento.DataSource = dt;
                cmbTipoMovimiento.DisplayMember = "NombreTipo";
                cmbTipoMovimiento.ValueMember = "ID_TipoMovimiento";
                cmbTipoMovimiento.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de movimiento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = movimientosDAO.ObtenerUsuarios();
                cmbUsuario.DataSource = dt;
                cmbUsuario.DisplayMember = "NombreUsuario";
                cmbUsuario.ValueMember = "ID_Usuarios";
                cmbUsuario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Movimientos movimiento = new Movimientos
                {
                    ID_Productos = Convert.ToInt32(cmbProducto.SelectedValue),
                    ID_Bodegas = Convert.ToInt32(cmbBodega.SelectedValue),
                    ID_TipoMovimiento = Convert.ToInt32(cmbTipoMovimiento.SelectedValue),
                    ID_Usuarios = Convert.ToInt32(cmbUsuario.SelectedValue),
                    Cantidad = Convert.ToDecimal(txtCantidad.Text),
                    FechaMovimiento = dtpFechaMovimiento.Value,
                    Comentario = txtComentario.Text,
                    ReferenciaExterna = txtReferenciaExterna.Text
                };

                movimientosDAO.Create(movimiento);
                inventarioDAO.UpdateStock(movimiento);
                MessageBox.Show("Movimiento agregado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimpiarCampos();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar movimiento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMovimientoId == 0)
                {
                    MessageBox.Show("Seleccione un registro para actualizar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                Movimientos movimiento = new Movimientos
                {
                    ID_Movimientos = selectedMovimientoId,
                    ID_Productos = Convert.ToInt32(cmbProducto.SelectedValue),
                    ID_Bodegas = Convert.ToInt32(cmbBodega.SelectedValue),
                    ID_TipoMovimiento = Convert.ToInt32(cmbTipoMovimiento.SelectedValue),
                    ID_Usuarios = Convert.ToInt32(cmbUsuario.SelectedValue),
                    Cantidad = Convert.ToDecimal(txtCantidad.Text),
                    FechaMovimiento = dtpFechaMovimiento.Value,
                    Comentario = txtComentario.Text,
                    ReferenciaExterna = txtReferenciaExterna.Text
                };

                movimientosDAO.Update(movimiento);
                MessageBox.Show("Movimiento actualizado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarMovimientos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar movimiento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedMovimientoId == 0)
                {
                    MessageBox.Show("Seleccione un registro para eliminar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este registro?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    movimientosDAO.Delete(selectedMovimientoId);
                    MessageBox.Show("Movimiento eliminado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    CargarMovimientos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar movimiento: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmbBuscarPor.Text) || string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    MessageBox.Show("Seleccione un criterio y un valor para buscar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = movimientosDAO.BuscarMovimientos(cmbBuscarPor.Text, txtBuscar.Text);

                if (dt.Rows.Count > 0)
                {
                    dgvMovimientos.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMovimientos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMovimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMovimientos.Rows[e.RowIndex];

                selectedMovimientoId = Convert.ToInt32(row.Cells["ID_Movimientos"].Value);
                cmbProducto.SelectedValue = Convert.ToInt32(row.Cells["ID_Productos"].Value);
                cmbBodega.SelectedValue = Convert.ToInt32(row.Cells["ID_Bodegas"].Value);
                cmbTipoMovimiento.SelectedValue = Convert.ToInt32(row.Cells["ID_TipoMovimiento"].Value);
                cmbUsuario.SelectedValue = Convert.ToInt32(row.Cells["ID_Usuarios"].Value);
                txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();
                dtpFechaMovimiento.Value = Convert.ToDateTime(row.Cells["FechaMovimiento"].Value);
                txtComentario.Text = row.Cells["Comentario"].Value?.ToString() ?? string.Empty;
                txtReferenciaExterna.Text = row.Cells["ReferenciaExterna"].Value?.ToString() ?? string.Empty;
            }
        }

        private bool ValidarCampos()
        {
            if (cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbBodega.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una bodega", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbTipoMovimiento.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de movimiento", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbUsuario.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un usuario", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese la cantidad", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("La cantidad debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            selectedMovimientoId = 0;
            cmbProducto.SelectedIndex = -1;
            cmbBodega.SelectedIndex = -1;
            cmbTipoMovimiento.SelectedIndex = -1;
            cmbUsuario.SelectedIndex = -1;
            txtCantidad.Clear();
            dtpFechaMovimiento.Value = DateTime.Now;
            txtComentario.Clear();
            txtReferenciaExterna.Clear();
            txtBuscar.Clear();
            cmbBuscarPor.SelectedIndex = -1;
        }
    }
}

