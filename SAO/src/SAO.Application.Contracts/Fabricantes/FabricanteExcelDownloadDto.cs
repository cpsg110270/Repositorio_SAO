using Volo.Abp.Application.Dtos;
using System;

namespace SAO.Fabricantes
{
    public class FabricanteExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombreFabricante { get; set; }

        public FabricanteExcelDownloadDto()
        {

        }
    }
}