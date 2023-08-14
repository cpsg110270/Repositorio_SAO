namespace SAO.TipoPermisos
{
    public class TipoPermisoExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? Codigo { get; set; }
        public string? Desripcion { get; set; }

        public TipoPermisoExcelDownloadDto()
        {

        }
    }
}