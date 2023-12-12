using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Exportadors
{
    public class GetExportadorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? NoImportadorMin { get; set; }
        public int? NoImportadorMax { get; set; }
        public string? NombreExportador { get; set; }

        public GetExportadorsInput()
        {

        }
    }
}