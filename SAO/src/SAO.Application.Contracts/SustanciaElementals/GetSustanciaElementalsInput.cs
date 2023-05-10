using Volo.Abp.Application.Dtos;
using System;

namespace SAO.SustanciaElementals
{
    public class GetSustanciaElementalsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? CodCas { get; set; }
        public string? DesSustancia { get; set; }

        public GetSustanciaElementalsInput()
        {

        }
    }
}