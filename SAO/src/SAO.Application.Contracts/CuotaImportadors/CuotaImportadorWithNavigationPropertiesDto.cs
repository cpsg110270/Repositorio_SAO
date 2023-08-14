using SAO.Importadors;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorWithNavigationPropertiesDto
    {
        public CuotaImportadorDto CuotaImportador { get; set; }

        public ImportadorDto Importador { get; set; }

    }
}