using System;

namespace SAO.ImporExports
{
    public class ImporExportExcelDto
    {
        public string NoPermiso { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public double PesoNeto { get; set; }
        public double PesoUnitario { get; set; }
        public int CantEnvvase { get; set; }
        public string NoFactura { get; set; }
        public string? Observaciones { get; set; }
        public bool EsRenovacion { get; set; }
        public bool Estado { get; set; }
    }
}