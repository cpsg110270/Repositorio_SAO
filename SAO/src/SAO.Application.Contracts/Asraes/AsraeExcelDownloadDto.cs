using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Asraes
{
    public class AsraeExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Codigo_ASHRAE { get; set; }
        public string? Descripcion { get; set; }

        public AsraeExcelDownloadDto()
        {

        }
    }
}