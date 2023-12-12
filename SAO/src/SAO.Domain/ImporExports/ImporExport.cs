using SAO.Importadors;
using SAO.Exportadors;
using SAO.Productos;
using SAO.UnidadMedidas;
using SAO.TipoEnvases;
using SAO.PuertoEntradaSalidas;
using SAO.Paiss;
using SAO.Almacens;
using SAO.ImporExports;
using SAO.TipoPermisos;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace SAO.ImporExports
{
    public class ImporExport : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string NoPermiso { get; set; }

        public virtual DateTime FechaEmision { get; set; }

        public virtual DateTime FechaSolicitud { get; set; }

        public virtual double PesoNeto { get; set; }

        public virtual double PesoUnitario { get; set; }

        public virtual int CantEnvvase { get; set; }

        [NotNull]
        public virtual string NoFactura { get; set; }

        [CanBeNull]
        public virtual string? Observaciones { get; set; }

        public virtual bool EsRenovacion { get; set; }

        public virtual bool Estado { get; set; }
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

        public ImporExport()
        {

        }

        public ImporExport(Guid id, Guid? importadorId, Guid exportadorId, Guid productoId, int unidadMedidaId, int tipoEnvaseId, int? puertoEntradaId, int? puertoSalidaId, int? paisProcedenciaId, int? paisDestinoId, int? paisOrigenId, int? almacenId, Guid? permisoRenov, Guid permisoDe, string noPermiso, DateTime fechaEmision, DateTime fechaSolicitud, double pesoNeto, double pesoUnitario, int cantEnvvase, string noFactura, string observaciones, bool esRenovacion, bool estado)
        {

            Id = id;
            Check.NotNull(noPermiso, nameof(noPermiso));
            Check.Length(noPermiso, nameof(noPermiso), ImporExportConsts.NoPermisoMaxLength, 0);
            Check.NotNull(noFactura, nameof(noFactura));
            Check.Length(noFactura, nameof(noFactura), ImporExportConsts.NoFacturaMaxLength, 0);
            Check.Length(observaciones, nameof(observaciones), ImporExportConsts.ObservacionesMaxLength, 0);
            NoPermiso = noPermiso;
            FechaEmision = fechaEmision;
            FechaSolicitud = fechaSolicitud;
            PesoNeto = pesoNeto;
            PesoUnitario = pesoUnitario;
            CantEnvvase = cantEnvvase;
            NoFactura = noFactura;
            Observaciones = observaciones;
            EsRenovacion = esRenovacion;
            Estado = estado;
            ImportadorId = importadorId;
            ExportadorId = exportadorId;
            ProductoId = productoId;
            UnidadMedidaId = unidadMedidaId;
            TipoEnvaseId = tipoEnvaseId;
            PuertoEntradaId = puertoEntradaId;
            PuertoSalidaId = puertoSalidaId;
            PaisProcedenciaId = paisProcedenciaId;
            PaisDestinoId = paisDestinoId;
            PaisOrigenId = paisOrigenId;
            AlmacenId = almacenId;
            PermisoRenov = permisoRenov;
            PermisoDe = permisoDe;
        }

    }
}