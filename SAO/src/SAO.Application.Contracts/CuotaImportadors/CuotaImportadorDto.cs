using System;
using Volo.Abp.Application.Dtos;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorDto : AuditedEntityDto<Guid>
    {
        public int Año { get; set; }
        public decimal Cuota { get; set; }
        public Guid ImportadorId { get; set; }

    }
}