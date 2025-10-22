namespace Proyecto_Zoologico.Formularios
{
    partial class Inventario_Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblBodega;
        private System.Windows.Forms.ComboBox cmbBodega;
        private System.Windows.Forms.Label lblCantidadStock;
        private System.Windows.Forms.TextBox txtCantidadStock;
        private System.Windows.Forms.Label lblPrecioVenta;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.Label lblStockMaximo;
        private System.Windows.Forms.TextBox txtStockMaximo;
        private System.Windows.Forms.Label lblUltimaActualizacion;
        private System.Windows.Forms.DateTimePicker dtpUltimaActualizacion;
        private System.Windows.Forms.GroupBox gbBusqueda;
        private System.Windows.Forms.Label lblBuscarPor;
        private System.Windows.Forms.ComboBox cmbBuscarPor;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbAcciones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnMovimientos;
        private System.Windows.Forms.DataGridView dgvInventario;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbBodega = new System.Windows.Forms.ComboBox();
            this.lblCantidadStock = new System.Windows.Forms.Label();
            this.txtCantidadStock = new System.Windows.Forms.TextBox();
            this.lblPrecioVenta = new System.Windows.Forms.Label();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.lblStockMaximo = new System.Windows.Forms.Label();
            this.txtStockMaximo = new System.Windows.Forms.TextBox();
            this.lblUltimaActualizacion = new System.Windows.Forms.Label();
            this.dtpUltimaActualizacion = new System.Windows.Forms.DateTimePicker();
            this.gbBusqueda = new System.Windows.Forms.GroupBox();
            this.lblBuscarPor = new System.Windows.Forms.Label();
            this.cmbBuscarPor = new System.Windows.Forms.ComboBox();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnProducto = new System.Windows.Forms.Button();
            this.gbAcciones = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnMovimientos = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.panelSuperior.SuspendLayout();
            this.gbDatos.SuspendLayout();
            this.gbBusqueda.SuspendLayout();
            this.gbAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1200, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(294, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de Inventario";
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.lblProducto);
            this.gbDatos.Controls.Add(this.cmbProducto);
            this.gbDatos.Controls.Add(this.lblBodega);
            this.gbDatos.Controls.Add(this.cmbBodega);
            this.gbDatos.Controls.Add(this.lblCantidadStock);
            this.gbDatos.Controls.Add(this.txtCantidadStock);
            this.gbDatos.Controls.Add(this.lblPrecioVenta);
            this.gbDatos.Controls.Add(this.txtPrecioVenta);
            this.gbDatos.Controls.Add(this.lblStockMinimo);
            this.gbDatos.Controls.Add(this.txtStockMinimo);
            this.gbDatos.Controls.Add(this.lblStockMaximo);
            this.gbDatos.Controls.Add(this.txtStockMaximo);
            this.gbDatos.Controls.Add(this.lblUltimaActualizacion);
            this.gbDatos.Controls.Add(this.dtpUltimaActualizacion);
            this.gbDatos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDatos.Location = new System.Drawing.Point(20, 80);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(560, 280);
            this.gbDatos.TabIndex = 1;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del Inventario";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProducto.Location = new System.Drawing.Point(15, 30);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(68, 19);
            this.lblProducto.TabIndex = 0;
            this.lblProducto.Text = "Producto:";
            // 
            // cmbProducto
            // 
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(15, 50);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(250, 25);
            this.cmbProducto.TabIndex = 1;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBodega.Location = new System.Drawing.Point(285, 30);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(58, 19);
            this.lblBodega.TabIndex = 2;
            this.lblBodega.Text = "Bodega:";
            // 
            // cmbBodega
            // 
            this.cmbBodega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBodega.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(285, 50);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(250, 25);
            this.cmbBodega.TabIndex = 3;
            // 
            // lblCantidadStock
            // 
            this.lblCantidadStock.AutoSize = true;
            this.lblCantidadStock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCantidadStock.Location = new System.Drawing.Point(15, 90);
            this.lblCantidadStock.Name = "lblCantidadStock";
            this.lblCantidadStock.Size = new System.Drawing.Size(104, 19);
            this.lblCantidadStock.TabIndex = 4;
            this.lblCantidadStock.Text = "Cantidad Stock:";
            // 
            // txtCantidadStock
            // 
            this.txtCantidadStock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCantidadStock.Location = new System.Drawing.Point(15, 110);
            this.txtCantidadStock.Name = "txtCantidadStock";
            this.txtCantidadStock.Size = new System.Drawing.Size(250, 25);
            this.txtCantidadStock.TabIndex = 5;
            // 
            // lblPrecioVenta
            // 
            this.lblPrecioVenta.AutoSize = true;
            this.lblPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPrecioVenta.Location = new System.Drawing.Point(285, 90);
            this.lblPrecioVenta.Name = "lblPrecioVenta";
            this.lblPrecioVenta.Size = new System.Drawing.Size(88, 19);
            this.lblPrecioVenta.TabIndex = 6;
            this.lblPrecioVenta.Text = "Precio Venta:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrecioVenta.Location = new System.Drawing.Point(285, 110);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(250, 25);
            this.txtPrecioVenta.TabIndex = 7;
            // 
            // lblStockMinimo
            // 
            this.lblStockMinimo.AutoSize = true;
            this.lblStockMinimo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockMinimo.Location = new System.Drawing.Point(15, 150);
            this.lblStockMinimo.Name = "lblStockMinimo";
            this.lblStockMinimo.Size = new System.Drawing.Size(96, 19);
            this.lblStockMinimo.TabIndex = 8;
            this.lblStockMinimo.Text = "Stock Mínimo:";
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStockMinimo.Location = new System.Drawing.Point(15, 170);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(250, 25);
            this.txtStockMinimo.TabIndex = 9;
            // 
            // lblStockMaximo
            // 
            this.lblStockMaximo.AutoSize = true;
            this.lblStockMaximo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockMaximo.Location = new System.Drawing.Point(285, 150);
            this.lblStockMaximo.Name = "lblStockMaximo";
            this.lblStockMaximo.Size = new System.Drawing.Size(98, 19);
            this.lblStockMaximo.TabIndex = 10;
            this.lblStockMaximo.Text = "Stock Máximo:";
            // 
            // txtStockMaximo
            // 
            this.txtStockMaximo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStockMaximo.Location = new System.Drawing.Point(285, 170);
            this.txtStockMaximo.Name = "txtStockMaximo";
            this.txtStockMaximo.Size = new System.Drawing.Size(250, 25);
            this.txtStockMaximo.TabIndex = 11;
            // 
            // lblUltimaActualizacion
            // 
            this.lblUltimaActualizacion.AutoSize = true;
            this.lblUltimaActualizacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUltimaActualizacion.Location = new System.Drawing.Point(15, 210);
            this.lblUltimaActualizacion.Name = "lblUltimaActualizacion";
            this.lblUltimaActualizacion.Size = new System.Drawing.Size(135, 19);
            this.lblUltimaActualizacion.TabIndex = 12;
            this.lblUltimaActualizacion.Text = "Última Actualización:";
            // 
            // dtpUltimaActualizacion
            // 
            this.dtpUltimaActualizacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpUltimaActualizacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpUltimaActualizacion.Location = new System.Drawing.Point(15, 230);
            this.dtpUltimaActualizacion.Name = "dtpUltimaActualizacion";
            this.dtpUltimaActualizacion.Size = new System.Drawing.Size(250, 25);
            this.dtpUltimaActualizacion.TabIndex = 13;
            // 
            // gbBusqueda
            // 
            this.gbBusqueda.Controls.Add(this.lblBuscarPor);
            this.gbBusqueda.Controls.Add(this.cmbBuscarPor);
            this.gbBusqueda.Controls.Add(this.txtBuscar);
            this.gbBusqueda.Controls.Add(this.btnBuscar);
            this.gbBusqueda.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbBusqueda.Location = new System.Drawing.Point(600, 80);
            this.gbBusqueda.Name = "gbBusqueda";
            this.gbBusqueda.Size = new System.Drawing.Size(280, 280);
            this.gbBusqueda.TabIndex = 2;
            this.gbBusqueda.TabStop = false;
            this.gbBusqueda.Text = "Búsqueda";
            // 
            // lblBuscarPor
            // 
            this.lblBuscarPor.AutoSize = true;
            this.lblBuscarPor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBuscarPor.Location = new System.Drawing.Point(15, 35);
            this.lblBuscarPor.Name = "lblBuscarPor";
            this.lblBuscarPor.Size = new System.Drawing.Size(77, 19);
            this.lblBuscarPor.TabIndex = 0;
            this.lblBuscarPor.Text = "Buscar por:";
            // 
            // cmbBuscarPor
            // 
            this.cmbBuscarPor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuscarPor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbBuscarPor.FormattingEnabled = true;
            this.cmbBuscarPor.Items.AddRange(new object[] {
            "ID_Inventario",
            "ID_Productos",
            "ID_Bodegas"});
            this.cmbBuscarPor.Location = new System.Drawing.Point(15, 55);
            this.cmbBuscarPor.Name = "cmbBuscarPor";
            this.cmbBuscarPor.Size = new System.Drawing.Size(250, 25);
            this.cmbBuscarPor.TabIndex = 1;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBuscar.Location = new System.Drawing.Point(15, 90);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(250, 25);
            this.txtBuscar.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(15, 125);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(250, 35);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnProducto
            // 
            this.btnProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnProducto.ForeColor = System.Drawing.Color.White;
            this.btnProducto.Location = new System.Drawing.Point(15, 134);
            this.btnProducto.Name = "btnProducto";
            this.btnProducto.Size = new System.Drawing.Size(250, 35);
            this.btnProducto.TabIndex = 4;
            this.btnProducto.Text = "➕ Agregar Producto";
            this.btnProducto.UseVisualStyleBackColor = false;
            this.btnProducto.Click += new System.EventHandler(this.btnProducto_Click);
            // 
            // gbAcciones
            // 
            this.gbAcciones.Controls.Add(this.btnProducto);
            this.gbAcciones.Controls.Add(this.btnAgregar);
            this.gbAcciones.Controls.Add(this.btnEliminar);
            this.gbAcciones.Controls.Add(this.btnMovimientos);
            this.gbAcciones.Controls.Add(this.btnActualizar);
            this.gbAcciones.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbAcciones.Location = new System.Drawing.Point(900, 80);
            this.gbAcciones.Name = "gbAcciones";
            this.gbAcciones.Size = new System.Drawing.Size(280, 280);
            this.gbAcciones.TabIndex = 3;
            this.gbAcciones.TabStop = false;
            this.gbAcciones.Text = "Acciones";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(15, 35);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(250, 35);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "➕ Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(15, 239);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(250, 35);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "GENERAR REPORTE";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnMovimientos
            // 
            this.btnMovimientos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovimientos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMovimientos.ForeColor = System.Drawing.Color.White;
            this.btnMovimientos.Location = new System.Drawing.Point(15, 184);
            this.btnMovimientos.Name = "btnMovimientos";
            this.btnMovimientos.Size = new System.Drawing.Size(250, 35);
            this.btnMovimientos.TabIndex = 3;
            this.btnMovimientos.Text = "🔄 Movimientos";
            this.btnMovimientos.UseVisualStyleBackColor = false;
            this.btnMovimientos.Click += new System.EventHandler(this.btnMovimientos_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(15, 85);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(250, 35);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "✏️ Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // dgvInventario
            // 
            this.dgvInventario.AllowUserToAddRows = false;
            this.dgvInventario.AllowUserToDeleteRows = false;
            this.dgvInventario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInventario.BackgroundColor = System.Drawing.Color.White;
            this.dgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventario.Location = new System.Drawing.Point(20, 380);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.ReadOnly = true;
            this.dgvInventario.RowHeadersWidth = 45;
            this.dgvInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInventario.Size = new System.Drawing.Size(1160, 260);
            this.dgvInventario.TabIndex = 4;
            this.dgvInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInventario_CellClick);
            // 
            // Inventario_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1200, 660);
            this.Controls.Add(this.dgvInventario);
            this.Controls.Add(this.gbAcciones);
            this.Controls.Add(this.gbBusqueda);
            this.Controls.Add(this.gbDatos);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Inventario_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Inventario";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            this.gbBusqueda.ResumeLayout(false);
            this.gbBusqueda.PerformLayout();
            this.gbAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnProducto;
    }
}