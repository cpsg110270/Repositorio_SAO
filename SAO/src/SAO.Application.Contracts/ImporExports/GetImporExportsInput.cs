using System;
using Volo.Abp.Application.Dtos;

namespace SAO.ImporExports
{
    public class GetImporExportsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? NoPermiso { get; set; }
        public DateTime? FechaEmisionMin { get; set; }
        public DateTime? FechaEmisionMax { get; set; }
        public DateTime? FechaSolicitudMin { get; set; }
        public DateTime? FechaSolicitudMax { get; set; }
        public double? PesoNetoMin { get; set; }
        public double? PesoNetoMax { get; set; }
        public double? PesoUnitarioMin { get; set; }
        public double? PesoUnitarioMax { get; set; }
        public int? CantEnvvaseMin { get; set; }
        public int? CantEnvvaseMax { get; set; }
        public string? NoFactura { get; set; }
        public string? Observaciones { get; set; }
        public bool? EsRenovacion { get; set; }
        public bool? Estado { get; set; }
        public Guid? ImportadorId { get; set; }
        public Guid? ExportadorId { get; set; }
        public Guid? ProductoId { get; set; }
        public int? UnidadMedidaId { get; set; }
        public int? TipoEnvaseId { get; set; }
        public int? PuertoEntradaId { get; set; }
        public int? PuertoSalidaId { get; set; }
        public int? PaisProcedenciaId { get; set; }
        public int? PaisDestinoId { get; set; }
        public int? PaisOrigenId { get; set; }
        public int? AlmacenId { get; set; }
        public Guid? PermisoRenov { get; set; }
        public Guid? PermisoDe { get; set; }

        public GetImporExportsInput()
        {

        }
    }
}