using System;
using Volo.Abp.Application.Dtos;

namespace SAO.TipoPermisos
{
    public class TipoPermisoDto : EntityDto<Guid>
    {
        public string Codigo { get; set; }
        public string? Desripcion { get; set; }

    }
}