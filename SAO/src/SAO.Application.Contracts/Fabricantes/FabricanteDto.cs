using System;
using Volo.Abp.Application.Dtos;

namespace SAO.Fabricantes
{
    public class FabricanteDto : EntityDto<Guid>
    {
        public string NombreFabricante { get; set; }

    }
}