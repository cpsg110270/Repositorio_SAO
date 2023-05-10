using System;
using Volo.Abp.Application.Dtos;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseDto : EntityDto<int>
    {
        public string DesEnvase { get; set; }

    }
}