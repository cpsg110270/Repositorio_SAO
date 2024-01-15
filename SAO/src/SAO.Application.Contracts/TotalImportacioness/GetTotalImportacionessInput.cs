using Volo.Abp.Application.Dtos;
using System;

namespace SAO.TotalImportacioness
{
    public class GetTotalImportacionessInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? AnioMin { get; set; }
        public int? AnioMax { get; set; }
        public double? CuotaAsignadaMin { get; set; }
        public double? CuotaAsignadaMax { get; set; }
        public double? CuotaConsumidaMin { get; set; }
        public double? CuotaConsumidaMax { get; set; }
        public Guid? ImportadorId { get; set; }
        public Guid? TipoProductoId { get; set; }
        public int? AsraeId { get; set; }

        public GetTotalImportacionessInput()
        {

        }
    }
}