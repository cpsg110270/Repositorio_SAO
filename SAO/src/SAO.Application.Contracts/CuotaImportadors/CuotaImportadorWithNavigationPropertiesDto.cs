using SAO.Importadors;
using SAO.Asraes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorWithNavigationPropertiesDto
    {
        public CuotaImportadorDto CuotaImportador { get; set; }

        public ImportadorDto Importador { get; set; }
        public AsraeDto Asrae { get; set; }

    }
}