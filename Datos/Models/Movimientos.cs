using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Movimientos
{
    public int ID_Movimientos { get; set; }
    public int ID_Productos { get; set; }
    public int ID_Bodegas { get; set; }
    public int ID_TipoMovimiento { get; set; }
    public int ID_Usuarios { get; set; }
    public decimal Cantidad { get; set; }
    public DateTime FechaMovimiento { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string ReferenciaExterna { get; set; } = string.Empty;
}