using SAO.Importadors;
using SAO.Exportadors;
using SAO.Productos;
using SAO.UnidadMedidas;
using SAO.TipoEnvases;
using SAO.PuertoEntradaSalidas;
using SAO.PuertoEntradaSalidas;
using SAO.Paiss;
using SAO.Paiss;
using SAO.Paiss;
using SAO.Almacens;
using SAO.ImporExports;
using SAO.TipoPermisos;

using System;
using System.Collections.Generic;

namespace SAO.ImporExports
{
    public class ImporExportWithNavigationProperties
    {
        public ImporExport ImporExport { get; set; }

        public Importador Importador { get; set; }
        public Exportador Exportador { get; set; }
        public Producto Producto { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public TipoEnvase TipoEnvase { get; set; }
        public PuertoEntradaSalida PuertoEntradaSalida { get; set; }
        public PuertoEntradaSalida PuertoEntradaSalida1 { get; set; }
        public Pais Pais { get; set; }
        public Pais Pais1 { get; set; }
        public Pais Pais2 { get; set; }
        public Almacen Almacen { get; set; }
        public ImporExport ImporExport1 { get; set; }
        public TipoPermiso TipoPermiso { get; set; }
        

        
    }
}