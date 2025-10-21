using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Inventario
{
    public int ID_Inventario { get; set; }
    public int ID_Productos { get; set; }
    public int ID_Bodegas { get; set; }
    public decimal CantidadStock { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal StockMinimo { get; set; }
    public decimal StockMaximo { get; set; }
    public DateTime UltimaActualizacion { get; set; } 
}

