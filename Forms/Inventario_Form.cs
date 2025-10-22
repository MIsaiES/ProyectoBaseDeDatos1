using Proyecto_BasedeDatos1;
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
                DataTable dt = inventarioDAO.GetAll();
                dgvInventario.DataSource = dt;

                
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
            try
            {
                if (selectedInventarioId == 0)
                {
                    MessageBox.Show("Seleccione un registro para actualizar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                Inventario inventario = new Inventario
                {
                    ID_Inventario = selectedInventarioId,
                    ID_Productos = Convert.ToInt32(cmbProducto.SelectedValue),
                    ID_Bodegas = inventarioDAO.getIdByName(cmbBodega.Text),
                    CantidadStock = Convert.ToDecimal(txtCantidadStock.Text),
                    PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text),
                    StockMinimo = Convert.ToDecimal(txtStockMinimo.Text),
                    StockMaximo = Convert.ToDecimal(txtStockMaximo.Text),
                    UltimaActualizacion = dtpUltimaActualizacion.Value
                };

                inventarioDAO.Update(inventario);
                MessageBox.Show("Inventario actualizado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarInventario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar inventario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ReporteInventarioPDF reporte = new ReporteInventarioPDF(ConexionSQL.cadenaConexion);
            reporte.GenerarReporteInventario();
            byte[] pdfBytes = reporte.GenerarReporteInventario();
            DateTime now = DateTime.Now;
            string hora = now.ToString("yyyyMMdd_HHmmss");
            string filePath = "C:\\Users\\Administrator\\Desktop\\reportes\\ReporteInventario" + hora + ".pdf";
            System.IO.File.WriteAllBytes(filePath, pdfBytes);
            MessageBox.Show($"Reporte de inventario generado en: {filePath}", "Reporte Generado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            var form = new Movimientos_Form();
            form.ShowDialog();
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
                cmbProducto.SelectedValue = row.Cells["Producto"].Value;
                cmbBodega.SelectedValue = row.Cells["Bodega"].Value;
                txtCantidadStock.Text = row.Cells["Stock"].Value.ToString();
                txtPrecioVenta.Text = row.Cells["Precio"].Value.ToString();
                txtStockMinimo.Text = row.Cells["Minimo"].Value.ToString();
                txtStockMaximo.Text = row.Cells["Maximo"].Value.ToString();
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

        private void btnProducto_Click(object sender, EventArgs e)
        {
            var form = new Productos_Form();
            form.ShowDialog();
        }

        
    }
}