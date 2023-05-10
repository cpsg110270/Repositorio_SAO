using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Paiss
{
    public class GetPaissInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombrePais { get; set; }

        public GetPaissInput()
        {

        }
    }
}