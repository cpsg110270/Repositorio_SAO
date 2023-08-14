namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidaExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombrePuerto { get; set; }

        public PuertoEntradaSalidaExcelDownloadDto()
        {

        }
    }
}