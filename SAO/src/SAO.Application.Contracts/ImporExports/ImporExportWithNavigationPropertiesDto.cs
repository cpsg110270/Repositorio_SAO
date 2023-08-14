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
    public class ImporExportWithNavigationPropertiesDto
    {
        public ImporExportDto ImporExport { get; set; }

        public ImportadorDto Importador { get; set; }
        public ExportadorDto Exportador { get; set; }
        public ProductoDto Producto { get; set; }
        public UnidadMedidaDto UnidadMedida { get; set; }
        public TipoEnvaseDto TipoEnvase { get; set; }
        public PuertoEntradaSalidaDto PuertoEntradaSalida { get; set; }
        public PuertoEntradaSalidaDto PuertoEntradaSalida1 { get; set; }
        public PaisDto Pais { get; set; }
        public PaisDto Pais1 { get; set; }
        public PaisDto Pais2 { get; set; }
        public AlmacenDto Almacen { get; set; }
        public ImporExportDto ImporExport1 { get; set; }
        public TipoPermisoDto TipoPermiso { get; set; }

    }
}