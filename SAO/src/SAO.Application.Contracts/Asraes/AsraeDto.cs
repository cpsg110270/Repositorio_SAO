using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Asraes
{
    public class AsraeDto : EntityDto<int>
    {
        public string Codigo_ASHRAE { get; set; }
        public string? Descripcion { get; set; }

    }
}