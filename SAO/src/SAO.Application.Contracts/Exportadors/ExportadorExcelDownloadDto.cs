namespace SAO.Exportadors
{
    public class ExportadorExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombreExportador { get; set; }

        public ExportadorExcelDownloadDto()
        {

        }
    }
}