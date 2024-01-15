using SAO.Importadors;
using SAO.TipoProductos;
using SAO.Asraes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesWithNavigationPropertiesDto
    {
        public TotalImportacionesDto TotalImportaciones { get; set; }

        public ImportadorDto Importador { get; set; }
        public TipoProductoDto TipoProducto { get; set; }
        public AsraeDto Asrae { get; set; }

    }
}