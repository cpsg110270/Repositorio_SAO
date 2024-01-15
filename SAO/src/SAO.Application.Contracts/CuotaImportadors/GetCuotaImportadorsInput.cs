using Volo.Abp.Application.Dtos;
using System;

namespace SAO.CuotaImportadors
{
    public class GetCuotaImportadorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? AñoMin { get; set; }
        public int? AñoMax { get; set; }
        public decimal? CuotaMin { get; set; }
        public decimal? CuotaMax { get; set; }
        public Guid? ImportadorId { get; set; }
        public int? AsraeId { get; set; }
        public Guid? TipoProductoId { get; set; }

        public GetCuotaImportadorsInput()
        {

        }
    }
}