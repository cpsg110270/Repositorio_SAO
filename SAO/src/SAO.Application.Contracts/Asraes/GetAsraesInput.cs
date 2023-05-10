using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Asraes
{
    public class GetAsraesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Codigo_ASHRAE { get; set; }
        public string? Descripcion { get; set; }

        public GetAsraesInput()
        {

        }
    }
}