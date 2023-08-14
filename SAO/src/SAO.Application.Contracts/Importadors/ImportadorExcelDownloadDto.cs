namespace SAO.Importadors
{
    public class ImportadorExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombreImportador { get; set; }

        public ImportadorExcelDownloadDto()
        {

        }
    }
}