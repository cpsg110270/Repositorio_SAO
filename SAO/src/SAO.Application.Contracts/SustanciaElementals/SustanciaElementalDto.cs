using System;
using Volo.Abp.Application.Dtos;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalDto : EntityDto<Guid>
    {
        public string CodCas { get; set; }
        public string DesSustancia { get; set; }

    }
}