using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Productos
{
    public int ID_Productos { get; set; }
    public int ID_TipoProducto { get; set; }
    public int ID_Marca { get; set; }
    public int ID_Proveedor { get; set; }
    public int ID_ProductoEstado { get; set; }
    public int ID_UniMed { get; set; }
    public int ID_UsuarioCreador { get; set; }
    public string Prod_SKU { get; set; } = string.Empty;
    public string Prod_Nombre { get; set; } = string.Empty;
    public string Prod_Descripcion { get; set; } = string.Empty;
    public decimal Prod_CostoUnitario { get; set; }
    public decimal? Prod_PorcentajeDescuentod { get; set; }
    public byte[] Prod_RutaImagen { get; set; }
    public DateTime Prod_FechaCreacion { get; set; }
    public DateTime? Prod_FechaModificacion { get; set; }
    public int ID_Categoria { get; set; }
}
