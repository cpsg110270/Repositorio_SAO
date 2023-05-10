using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Importadors
{
    public class GetImportadorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombreImportador { get; set; }

        public GetImportadorsInput()
        {

        }
    }
}