using Volo.Abp.Application.Dtos;
using System;

namespace SAO.TipoEnvases
{
    public class GetTipoEnvasesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? DesEnvase { get; set; }

        public GetTipoEnvasesInput()
        {

        }
    }
}