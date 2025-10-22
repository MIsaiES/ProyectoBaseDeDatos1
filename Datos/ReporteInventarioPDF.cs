using System;
using System.Data.SqlClient;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Colors;


namespace Proyecto_BasedeDatos1
{
    public class ReporteInventarioPDF
    {
        private readonly string _connectionString;

        public ReporteInventarioPDF(string connectionString)
        {
            _connectionString = connectionString;
        }

        public byte[] GenerarReporteInventario()
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new PdfWriter(memoryStream))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf))
            {
                //ENCABEZADO DE LA EMPRESA
                var encabezadoEmpresa = new Paragraph("DevSolutions")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(5);
                document.Add(encabezadoEmpresa);

                var subtitulo = new Paragraph("Sistema de Gestión de Inventario")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(14)
                    .SetMarginBottom(15);
                document.Add(subtitulo);

                var linea = new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine());
                linea.SetMarginBottom(10);
                document.Add(linea);

                //TITULO DEL REPORTE
                var titulo = new Paragraph("Reporte de Inventario")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(18)
                    .SetMarginBottom(20);
                document.Add(titulo);

                //TABLA DE DATOS
                var tabla = new Table(iText.Layout.Properties.UnitValue.CreatePercentArray(new float[] { 1, 3, 1, 2, 2 }))
                    .UseAllAvailableWidth();
                var headerStyle = new Style()
                    .SetFontSize(11)
                    .SetBackgroundColor(ColorConstants.LIGHT_GRAY);

                // Encabezados de columna
                tabla.AddHeaderCell(new Cell().Add(new Paragraph("SKU").AddStyle(headerStyle)));
                tabla.AddHeaderCell(new Cell().Add(new Paragraph("Nombre del Producto").AddStyle(headerStyle)));
                tabla.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").AddStyle(headerStyle)));
                tabla.AddHeaderCell(new Cell().Add(new Paragraph("Costo Unitario").AddStyle(headerStyle)));
                tabla.AddHeaderCell(new Cell().Add(new Paragraph("Precio de Venta").AddStyle(headerStyle)));

                //OBTENER DATOS DE LA BASE DE DATOS 
                const string sql = @"
                    SELECT 
                        p.Prod_SKU AS SKU,
                        p.Prod_Nombre AS NombreProducto,
                        i.CantidadStock AS Cantidad,
                        p.Prod_CostoUnitario AS CostoUnitario,
                        i.PrecioVenta AS PrecioVenta
                    FROM 
                        Tb_Productos p
                    INNER JOIN 
                        Tb_Inventario i ON p.ID_Productos = i.ID_Productos;";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tabla.AddCell(reader["SKU"].ToString());
                            tabla.AddCell(reader["NombreProducto"].ToString());
                            tabla.AddCell(reader["Cantidad"].ToString());
                            tabla.AddCell($"${reader.GetDecimal(reader.GetOrdinal("CostoUnitario")):N2}");
                            tabla.AddCell($"${reader.GetDecimal(reader.GetOrdinal("PrecioVenta")):N2}");
                        }
                    }
                }

                document.Add(tabla);
                var fecha = new Paragraph($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFontSize(9)
                    .SetMarginTop(20);
                document.Add(fecha);

                document.Close();
                return memoryStream.ToArray();
            }
        }
    }
}