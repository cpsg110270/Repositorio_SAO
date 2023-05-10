using Volo.Abp.Application.Dtos;
using System;

namespace SAO.TipoEnvases
{
    public class TipoEnvaseExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? DesEnvase { get; set; }

        public TipoEnvaseExcelDownloadDto()
        {

        }
    }
}