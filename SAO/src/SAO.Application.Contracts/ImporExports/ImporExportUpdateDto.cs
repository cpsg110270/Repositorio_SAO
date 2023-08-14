using System;
using System.ComponentModel.DataAnnotations;

namespace SAO.ImporExports
{
    public class ImporExportUpdateDto
    {
        [Required]
        [StringLength(ImporExportConsts.NoPermisoMaxLength)]
        public string NoPermiso { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public double PesoNeto { get; set; }
        public double PesoUnitario { get; set; }
        public int CantEnvvase { get; set; }
        [Required]
        [StringLength(ImporExportConsts.NoFacturaMaxLength)]
        public string NoFactura { get; set; }
        [StringLength(ImporExportConsts.ObservacionesMaxLength)]
        public string? Observaciones { get; set; }
        public bool EsRenovacion { get; set; }
        public bool Estado { get; set; }
        public Guid? ImportadorId { get; set; }
        public Guid ExportadorId { get; set; }
        public Guid ProductoId { get; set; }
        public int UnidadMedidaId { get; set; }
        public int TipoEnvaseId { get; set; }
        public int? PuertoEntradaId { get; set; }
        public int? PuertoSalidaId { get; set; }
        public int? PaisProcedenciaId { get; set; }
        public int? PaisDestinoId { get; set; }
        public int? PaisOrigenId { get; set; }
        public int? AlmacenId { get; set; }
        public Guid? PermisoRenov { get; set; }
        public Guid PermisoDe { get; set; }

    }
}