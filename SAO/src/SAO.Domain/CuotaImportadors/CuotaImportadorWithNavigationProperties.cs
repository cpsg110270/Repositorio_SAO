using SAO.Importadors;
using SAO.Asraes;
using SAO.TipoProductos;

using System;
using System.Collections.Generic;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorWithNavigationProperties
    {
        public CuotaImportador CuotaImportador { get; set; }

        public Importador Importador { get; set; }
        public Asrae Asrae { get; set; }
        public TipoProducto TipoProducto { get; set; }
        

        
    }
}