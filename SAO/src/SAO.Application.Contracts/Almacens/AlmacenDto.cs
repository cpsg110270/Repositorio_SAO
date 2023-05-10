using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Almacens
{
    public class AlmacenDto : EntityDto<int>
    {
        public string NombreAlmacen { get; set; }
        public string? SiglaAlmacen { get; set; }

    }
}