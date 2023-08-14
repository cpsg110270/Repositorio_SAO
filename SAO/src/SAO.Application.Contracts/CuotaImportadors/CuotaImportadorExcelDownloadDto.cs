using System;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public int? AñoMin { get; set; }
        public int? AñoMax { get; set; }
        public decimal? CuotaMin { get; set; }
        public decimal? CuotaMax { get; set; }
        public Guid? ImportadorId { get; set; }

        public CuotaImportadorExcelDownloadDto()
        {

        }
    }
}