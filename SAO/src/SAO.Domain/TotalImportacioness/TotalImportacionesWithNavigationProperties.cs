using SAO.Importadors;
using SAO.TipoProductos;
using SAO.Asraes;

using System;
using System.Collections.Generic;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesWithNavigationProperties
    {
        public TotalImportaciones TotalImportaciones { get; set; }

        public Importador Importador { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public Asrae Asrae { get; set; }
        

        
    }
}