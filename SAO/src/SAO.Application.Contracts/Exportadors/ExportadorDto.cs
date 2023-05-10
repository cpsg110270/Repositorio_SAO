using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Exportadors
{
    public class ExportadorDto : EntityDto<Guid>
    {
        public string NombreExportador { get; set; }

    }
}