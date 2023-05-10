using Volo.Abp.Application.Dtos;
using System;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? CodCas { get; set; }
        public string? DesSustancia { get; set; }

        public SustanciaElementalExcelDownloadDto()
        {

        }
    }
}