using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Exportadors
{
    public class ExportadorDto : EntityDto<Guid>
    {
        public int NoImportador { get; set; }
        public string NombreExportador { get; set; }

    }
}