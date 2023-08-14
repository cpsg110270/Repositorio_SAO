namespace SAO.Paiss
{
    public class PaisExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombrePais { get; set; }

        public PaisExcelDownloadDto()
        {

        }
    }
}