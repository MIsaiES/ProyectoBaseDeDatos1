using ProyectoBaseDeDatos1.Datos;
using ProyectoBaseDeDatos1.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Proyecto_Zoologico.Formularios
{
    public partial class Inventario_Form : Form
    {
        private InventarioDAO inventarioDAO = new InventarioDAO();
        private ProductosDAO productosDAO = new ProductosDAO();
        private int selectedInventarioId = 0;

        public Inventario_Form()
        {
            InitializeComponent();
            CargarInventario();
            CargarProductos();
            CargarBodegas();
            dtpUltimaActualizacion.Value = DateTime.Now;
        }

        private void CargarInventario()
        {
            try
            {
                if (dgvInventario.DataSource == null)
                {
                    return;
                }
                List<Inventario> dt = inventarioDAO.GetAll();
                dgvInventario.DataSource = dt;

                // Configurar columnas
                dgvInventario.Columns["ID_Inventario"].HeaderText = "ID";
                dgvInventario.Columns["ID_Inventario"].Width = 50;
                dgvInventario.Columns["ID_Productos"].Visible = false;
                dgvInventario.Columns["Producto"].HeaderText = "Producto";
                dgvInventario.Columns["ID_Bodegas"].Visible = false;
                dgvInventario.Columns["Bodega"].HeaderText = "Bodega";
                dgvInventario.Columns["CantidadStock"].HeaderText = "Cantidad Stock";
                dgvInventario.Columns["PrecioVenta"].HeaderText = "Precio Venta";
                dgvInventario.Columns["PrecioVenta"].DefaultCellStyle.Format = "C2";
                dgvInventario.Columns["StockMinimo"].HeaderText = "Stock Mínimo";
                dgvInventario.Columns["StockMaximo"].HeaderText = "Stock Máximo";
                dgvInventario.Columns["UltimaActualizacion"].HeaderText = "Última Actualización";
                dgvInventario.Columns["UltimaActualizacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                List<Productos> dt = productosDAO.GetAll();
                cmbProducto.DataSource = dt;
                cmbProducto.DisplayMember = "NombreProducto";
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
                List<string> dt = inventarioDAO.getBodegas();

                foreach (var item in dt)
                {
                    cmbBodega.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar bodegas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Inventario inventario = new Inventario
                {
                    ID_Productos = Convert.ToInt32(cmbProducto.SelectedValue),
                    ID_Bodegas = inventarioDAO.getIdByName(cmbBodega.Text),
                    CantidadStock = Convert.ToDecimal(txtCantidadStock.Text),
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                    StockMinimo = Convert.ToDecimal(txtStockMinimo.Text),
                    StockMaximo = Convert.ToDecimal(txtStockMaximo.Text),
                    UltimaActualizacion = dtpUltimaActualizacion.Value
                };

                inventarioDAO.Create(inventario);
                MessageBox.Show("Inventario agregado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (selectedInventarioId == 0)
            //    {
            //        MessageBox.Show("Seleccione un registro para actualizar", "Advertencia",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }

            //    if (!ValidarCampos())
            //        return;

            //    Inventario inventario = new Inventario
            //    {
            //        ID_Inventario = selectedInventarioId,
            //        ID_Productos = Convert.ToInt32(cmbProducto.SelectedValue),
            //        ID_Bodegas = inventarioDAO.getIdByName(cmbBodega.Text),
            //        CantidadStock = Convert.ToDecimal(txtCantidadStock.Text),
            //        PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
            //        StockMinimo = Convert.ToDecimal(txtStockMinimo.Text),
            //        StockMaximo = Convert.ToDecimal(txtStockMaximo.Text),
            //        UltimaActualizacion = dtpUltimaActualizacion.Value
            //    };

            //    inventarioDAO.Update(inventario);
            //    MessageBox.Show("Inventario actualizado exitosamente", "Éxito",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    LimpiarCampos();
            //    CargarInventario();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error al actualizar inventario: {ex.Message}", "Error",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
           var form = new Movimientos_Form();
            form.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedInventarioId == 0)
                {
                    MessageBox.Show("Seleccione un registro para eliminar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este registro?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    inventarioDAO.Delete(selectedInventarioId);
                    MessageBox.Show("Inventario eliminado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    CargarInventario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar inventario: {ex.Message}", "Error",
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

                List<Inventario> dt = inventarioDAO.getByParameter(cmbBuscarPor.Text, txtBuscar.Text);

                dgvInventario.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvInventario.Rows[e.RowIndex];

                selectedInventarioId = Convert.ToInt32(row.Cells["ID_Inventario"].Value);
                cmbProducto.SelectedValue = Convert.ToInt32(row.Cells["ID_Productos"].Value);
                cmbBodega.SelectedValue = Convert.ToInt32(row.Cells["ID_Bodegas"].Value);
                txtCantidadStock.Text = row.Cells["CantidadStock"].Value.ToString();
                txtPrecioVenta.Text = row.Cells["PrecioVenta"].Value.ToString();
                txtStockMinimo.Text = row.Cells["StockMinimo"].Value.ToString();
                txtStockMaximo.Text = row.Cells["StockMaximo"].Value.ToString();
                dtpUltimaActualizacion.Value = Convert.ToDateTime(row.Cells["UltimaActualizacion"].Value);
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

            if (string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                MessageBox.Show("Ingrese la cantidad en stock", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Ingrese el precio de venta", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStockMinimo.Text))
            {
                MessageBox.Show("Ingrese el stock mínimo", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStockMaximo.Text))
            {
                MessageBox.Show("Ingrese el stock máximo", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que sean números decimales válidos
            if (!decimal.TryParse(txtCantidadStock.Text, out _))
            {
                MessageBox.Show("La cantidad en stock debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out _))
            {
                MessageBox.Show("El precio de venta debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtStockMinimo.Text, out _))
            {
                MessageBox.Show("El stock mínimo debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtStockMaximo.Text, out _))
            {
                MessageBox.Show("El stock máximo debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            selectedInventarioId = 0;
            cmbProducto.SelectedIndex = -1;
            cmbBodega.SelectedIndex = -1;
            txtCantidadStock.Clear();
            txtPrecioVenta.Clear();
            txtStockMinimo.Clear();
            txtStockMaximo.Clear();
            dtpUltimaActualizacion.Value = DateTime.Now;
            txtBuscar.Clear();
            cmbBuscarPor.SelectedIndex = -1;
        }
    }
}