using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TiposMovimiento
{
    public int ID_TipoMovimiento { get; set; }
    public string CodigoTipo { get; set; } = string.Empty;
    public string NombreTipo { get; set; } = string.Empty;
    public string Descripcion { get; set; }
    public bool AfectaStock { get; set; }
    public string SignoMovimiento { get; set; } = string.Empty;
}