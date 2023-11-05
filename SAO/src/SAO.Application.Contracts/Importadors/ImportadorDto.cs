using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Importadors
{
    public class ImportadorDto : EntityDto<Guid>
    {
        public string NombreImportador { get; set; }
        public string? NoRUC { get; set; }

    }
}