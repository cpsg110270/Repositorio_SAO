namespace SAO.Almacens
{
    public class AlmacenExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? NombreAlmacen { get; set; }
        public string? SiglaAlmacen { get; set; }

        public AlmacenExcelDownloadDto()
        {

        }
    }
}