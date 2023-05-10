using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Almacens
{
    public class GetAlmacensInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NombreAlmacen { get; set; }
        public string? SiglaAlmacen { get; set; }

        public GetAlmacensInput()
        {

        }
    }
}