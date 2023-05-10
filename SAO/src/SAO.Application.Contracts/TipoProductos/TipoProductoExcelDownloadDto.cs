using Volo.Abp.Application.Dtos;
using System;

namespace SAO.TipoProductos
{
    public class TipoProductoExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? DesProducto { get; set; }

        public TipoProductoExcelDownloadDto()
        {

        }
    }
}