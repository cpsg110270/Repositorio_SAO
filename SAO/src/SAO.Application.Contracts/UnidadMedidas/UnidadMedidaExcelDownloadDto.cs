namespace SAO.UnidadMedidas
{
    public class UnidadMedidaExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Abreviatura { get; set; }
        public string? NombreUnidad { get; set; }

        public UnidadMedidaExcelDownloadDto()
        {

        }
    }
}