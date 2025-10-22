using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_Zoologico.Formularios
{
    public partial class Productos_Form : Form
    {
        private ProductosDAO productosDAO;
        private int selectedProductoId = 0;
        public string rutaImagenSeleccionada = "";

        public Productos_Form()
        {
            InitializeComponent();
            productosDAO = new ProductosDAO();
            CargarProductos();
            CargarTiposProducto();
            CargarMarcas();
            CargarProveedores();
            CargarEstados();
            CargarUnidadesMedida();
            CargarUsuarios();
            CargarCategorias();
        }

        private void CargarProductos()
        {
            try
            {

                DataTable dt = productosDAO.ObtenerProductos();
                dgvProductos.DataSource = dt;

                

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTiposProducto()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerTiposProducto();
                cmbTipoProducto.DataSource = dt;
                cmbTipoProducto.DisplayMember = "TipoProdNombre";
                cmbTipoProducto.ValueMember = "ID_TipoProducto";
                cmbTipoProducto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar tipos de producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMarcas()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerMarcas();
                cmbMarca.DataSource = dt;
                cmbMarca.DisplayMember = "MarcaNombre";
                cmbMarca.ValueMember = "ID_Marca";
                cmbMarca.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar marcas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProveedores()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerProveedores();
                cmbProveedor.DataSource = dt;
                cmbProveedor.DisplayMember = "ProvNombre";
                cmbProveedor.ValueMember = "ID_Proveedor";
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar proveedores: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEstados()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerEstados();
                cmbEstado.DataSource = dt;
                cmbEstado.DisplayMember = "EstadoProdNombre";
                cmbEstado.ValueMember = "ID_ProductoEstado";
                cmbEstado.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estados: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUnidadesMedida()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerUnidadesMedida();
                cmbUnidadMedida.DataSource = dt;
                cmbUnidadMedida.DisplayMember = "NombreUniMed";
                cmbUnidadMedida.ValueMember = "ID_UniMed";
                cmbUnidadMedida.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar unidades de medida: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerUsuarios();
                cmbUsuarioCreador.DataSource = dt;
                cmbUsuarioCreador.DisplayMember = "NombreUsuario";
                cmbUsuarioCreador.ValueMember = "ID_Usuarios";
                cmbUsuarioCreador.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarCategorias()
        {
            try
            {
                DataTable dt = productosDAO.ObtenerCategorias();

                cmbCategoria.DataSource = dt;
                cmbCategoria.DisplayMember = "NombreCategoria";
                cmbCategoria.ValueMember = "ID_Categorias";
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;
                byte[] imagenBytes = File.ReadAllBytes(rutaImagenSeleccionada);
                Productos producto = new Productos
                {
                    ID_TipoProducto = Convert.ToInt32(cmbTipoProducto.SelectedValue),
                    ID_Marca = Convert.ToInt32(cmbMarca.SelectedValue),
                    ID_Proveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    ID_ProductoEstado = Convert.ToInt32(cmbEstado.SelectedValue),
                    ID_UniMed = Convert.ToInt32(cmbUnidadMedida.SelectedValue),
                    ID_UsuarioCreador = Convert.ToInt32(cmbUsuarioCreador.SelectedValue),
                    Prod_SKU = txtSKU.Text.Trim(),
                    Prod_Nombre = txtNombre.Text.Trim(),
                    Prod_Descripcion = txtDescripcion.Text.Trim(),
                    Prod_CostoUnitario = Convert.ToDecimal(txtCostoUnitario.Text),
                    Prod_PorcentajeDescuentod = string.IsNullOrWhiteSpace(txtDescuento.Text) ?
                        (decimal?)null : Convert.ToDecimal(txtDescuento.Text),
                    Prod_FechaCreacion = DateTime.Now,
                    Prod_FechaModificacion = null,
                    Prod_RutaImagen = string.IsNullOrWhiteSpace(rutaImagenSeleccionada) ?
                        null : System.IO.File.ReadAllBytes(rutaImagenSeleccionada),
                    ID_Categoria =  Convert.ToInt32(cmbCategoria.SelectedValue) 
                };

                productosDAO.Create(producto);
                MessageBox.Show("Producto agregado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedProductoId == 0)
                {
                    MessageBox.Show("Seleccione un registro para actualizar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                Productos producto = new Productos
                {
                    ID_Productos = selectedProductoId,
                    ID_TipoProducto = Convert.ToInt32(cmbTipoProducto.SelectedValue),
                    ID_Marca = Convert.ToInt32(cmbMarca.SelectedValue),
                    ID_Proveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    ID_ProductoEstado = Convert.ToInt32(cmbEstado.SelectedValue),
                    ID_UniMed = Convert.ToInt32(cmbUnidadMedida.SelectedValue),
                    ID_UsuarioCreador = Convert.ToInt32(cmbUsuarioCreador.SelectedValue),
                    Prod_SKU = txtSKU.Text.Trim(),
                    Prod_Nombre = txtNombre.Text.Trim(),
                    Prod_Descripcion = txtDescripcion.Text.Trim(),
                    Prod_CostoUnitario = Convert.ToDecimal(txtCostoUnitario.Text),
                    Prod_PorcentajeDescuentod = string.IsNullOrWhiteSpace(txtDescuento.Text) ?
                        (decimal?)null : Convert.ToDecimal(txtDescuento.Text),
                    Prod_FechaCreacion = DateTime.Now,
                    Prod_FechaModificacion = DateTime.Now,
                    Prod_RutaImagen = string.IsNullOrEmpty(rutaImagenSeleccionada) ? null :
                        System.IO.File.ReadAllBytes(rutaImagenSeleccionada),
                    ID_Categoria = Convert.ToInt32(cmbCategoria.SelectedValue)
                };

                productosDAO.Update(producto);
                MessageBox.Show("Producto actualizado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar producto: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedProductoId == 0)
                {
                    MessageBox.Show("Seleccione un registro para eliminar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar este registro?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    productosDAO.Delete(selectedProductoId);
                    MessageBox.Show("Producto eliminado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimpiarCampos();
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error",
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

                DataTable dt = productosDAO.BuscarProductos(cmbBuscarPor.Text, txtBuscar.Text);

                if (dt.Rows.Count > 0)
                {
                    dgvProductos.DataSource = dt;
                    var imgObj = dt.Rows[0]["Imagen"];
                    if (imgObj != DBNull.Value && imgObj is byte[] imgBytes && imgBytes.Length > 0)
                    {
                        using (var ms = new MemoryStream(imgBytes))
                        {
                            pictureBoxProducto.Image = Image.FromStream(ms);
                            pictureBoxProducto.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    else
                    {
                        pictureBoxProducto.Image = null;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvProductos.Rows[e.RowIndex];

                    selectedProductoId = Convert.ToInt32(row.Cells["ID_Productos"].Value);

                    // Cargar datos básicos
                    txtSKU.Text = row.Cells["SKU"].Value?.ToString() ?? string.Empty;
                    txtNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? string.Empty;
                    txtDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? string.Empty;
                    txtCostoUnitario.Text = row.Cells["CostoUnitario"].Value?.ToString() ?? "0";
                    txtDescuento.Text = row.Cells["Descuento"].Value?.ToString() ?? string.Empty;

                    // Cargar ComboBoxes (necesitamos obtener el producto completo para los IDs)
                    Productos productoCompleto = productosDAO.GetById(selectedProductoId);
                    if (productoCompleto != null)
                    {
                        cmbTipoProducto.SelectedValue = productoCompleto.ID_TipoProducto;
                        cmbMarca.SelectedValue = productoCompleto.ID_Marca;
                        cmbProveedor.SelectedValue = productoCompleto.ID_Proveedor;
                        cmbEstado.SelectedValue = productoCompleto.ID_ProductoEstado;
                        cmbUnidadMedida.SelectedValue = productoCompleto.ID_UniMed;
                        cmbUsuarioCreador.SelectedValue = productoCompleto.ID_UsuarioCreador;
                        pictureBoxProducto.Image = Image.FromStream(new MemoryStream(productoCompleto.Prod_RutaImagen));
                        pictureBoxProducto.SizeMode = PictureBoxSizeMode.Zoom;
                        cmbCategoria.SelectedValue = productoCompleto.ID_Categoria;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtSKU.Text))
            {
                MessageBox.Show("Ingrese el SKU del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese la descripción del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbTipoProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una marca", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un proveedor", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbUnidadMedida.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una unidad de medida", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbUsuarioCreador.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un usuario creador", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCostoUnitario.Text))
            {
                MessageBox.Show("Ingrese el costo unitario", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtCostoUnitario.Text, out _))
            {
                MessageBox.Show("El costo unitario debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtDescuento.Text) && !decimal.TryParse(txtDescuento.Text, out _))
            {
                MessageBox.Show("El descuento debe ser un número válido", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            selectedProductoId = 0;
            txtSKU.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            cmbTipoProducto.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
            cmbUnidadMedida.SelectedIndex = -1;
            cmbUsuarioCreador.SelectedIndex = -1;
            txtCostoUnitario.Clear();
            txtDescuento.Clear();
            txtBuscar.Clear();
            cmbBuscarPor.SelectedIndex = -1;
        }

        private void buttonCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFile.Title = "Seleccionar Imagen";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                rutaImagenSeleccionada = openFile.FileName;
                pictureBoxProducto.Image = Image.FromFile(rutaImagenSeleccionada);
                pictureBoxProducto.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}