using System;
using Volo.Abp.Application.Dtos;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionesDto : FullAuditedEntityDto<Guid>
    {
        public int Anio { get; set; }
        public double CuotaAsignada { get; set; }
        public double? CuotaConsumida { get; set; }
        public Guid ImportadorId { get; set; }
        public Guid TipoProductoId { get; set; }
        public int AsraeId { get; set; }

    }
}