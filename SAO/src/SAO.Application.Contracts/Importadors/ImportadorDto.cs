using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Importadors
{
    public class ImportadorDto : EntityDto<Guid>
    {
        public int NoImportador { get; set; }
        public string? NoRUC { get; set; }
        public string NombreImportador { get; set; }

    }
}