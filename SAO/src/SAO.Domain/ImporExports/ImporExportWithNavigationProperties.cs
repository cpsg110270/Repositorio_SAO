using SAO.Almacens;
using SAO.Exportadors;
using SAO.Importadors;
using SAO.Paiss;
using SAO.Productos;
using SAO.PuertoEntradaSalidas;
using SAO.TipoEnvases;
using SAO.TipoPermisos;
using SAO.UnidadMedidas;

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