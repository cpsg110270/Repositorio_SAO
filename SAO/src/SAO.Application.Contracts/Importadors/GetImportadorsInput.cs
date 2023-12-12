using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Importadors
{
    public class GetImportadorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? NoImportadorMin { get; set; }
        public int? NoImportadorMax { get; set; }
        public string? NoRUC { get; set; }
        public string? NombreImportador { get; set; }

        public GetImportadorsInput()
        {

        }
    }
}