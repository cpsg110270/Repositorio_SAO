using System;
using System.Collections.Generic;
using System.Text;

namespace SAO.Reportes
{
    public class RepCuotasImportadoresDto
    {
        public int Año { get; set; }
        public decimal Cuota { get; set; }
        public decimal PesoNeto { get; set; }
        public string Importador { get; set; }
    }
}
