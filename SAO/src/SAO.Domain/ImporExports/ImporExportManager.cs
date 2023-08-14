using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.ImporExports
{
    public class ImporExportManager : DomainService
    {
        private readonly IImporExportRepository _imporExportRepository;

        public ImporExportManager(IImporExportRepository imporExportRepository)
        {
            _imporExportRepository = imporExportRepository;
        }

        public async Task<ImporExport> CreateAsync(
        Guid? importadorId, Guid exportadorId, Guid productoId, int unidadMedidaId, int tipoEnvaseId, int? puertoEntradaId, int? puertoSalidaId, int? paisProcedenciaId, int? paisDestinoId, int? paisOrigenId, int? almacenId, Guid? permisoRenov, Guid permisoDe, string noPermiso, DateTime fechaEmision, DateTime fechaSolicitud, double pesoNeto, double pesoUnitario, int cantEnvvase, string noFactura, string observaciones, bool esRenovacion, bool estado)
        {
            Check.NotNull(exportadorId, nameof(exportadorId));
            Check.NotNull(productoId, nameof(productoId));
            Check.NotNull(unidadMedidaId, nameof(unidadMedidaId));
            Check.NotNull(tipoEnvaseId, nameof(tipoEnvaseId));
            Check.NotNull(permisoDe, nameof(permisoDe));
            Check.NotNullOrWhiteSpace(noPermiso, nameof(noPermiso));
            Check.Length(noPermiso, nameof(noPermiso), ImporExportConsts.NoPermisoMaxLength);
            Check.NotNull(fechaEmision, nameof(fechaEmision));
            Check.NotNull(fechaSolicitud, nameof(fechaSolicitud));
            Check.NotNullOrWhiteSpace(noFactura, nameof(noFactura));
            Check.Length(noFactura, nameof(noFactura), ImporExportConsts.NoFacturaMaxLength);
            Check.Length(observaciones, nameof(observaciones), ImporExportConsts.ObservacionesMaxLength);

            var imporExport = new ImporExport(
             GuidGenerator.Create(),
             importadorId, exportadorId, productoId, unidadMedidaId, tipoEnvaseId, puertoEntradaId, puertoSalidaId, paisProcedenciaId, paisDestinoId, paisOrigenId, almacenId, permisoRenov, permisoDe, noPermiso, fechaEmision, fechaSolicitud, pesoNeto, pesoUnitario, cantEnvvase, noFactura, observaciones, esRenovacion, estado
             );

            return await _imporExportRepository.InsertAsync(imporExport);
        }

        public async Task<ImporExport> UpdateAsync(
            Guid id,
            Guid? importadorId, Guid exportadorId, Guid productoId, int unidadMedidaId, int tipoEnvaseId, int? puertoEntradaId, int? puertoSalidaId, int? paisProcedenciaId, int? paisDestinoId, int? paisOrigenId, int? almacenId, Guid? permisoRenov, Guid permisoDe, string noPermiso, DateTime fechaEmision, DateTime fechaSolicitud, double pesoNeto, double pesoUnitario, int cantEnvvase, string noFactura, string observaciones, bool esRenovacion, bool estado
        )
        {
            Check.NotNull(exportadorId, nameof(exportadorId));
            Check.NotNull(productoId, nameof(productoId));
            Check.NotNull(unidadMedidaId, nameof(unidadMedidaId));
            Check.NotNull(tipoEnvaseId, nameof(tipoEnvaseId));
            Check.NotNull(permisoDe, nameof(permisoDe));
            Check.NotNullOrWhiteSpace(noPermiso, nameof(noPermiso));
            Check.Length(noPermiso, nameof(noPermiso), ImporExportConsts.NoPermisoMaxLength);
            Check.NotNull(fechaEmision, nameof(fechaEmision));
            Check.NotNull(fechaSolicitud, nameof(fechaSolicitud));
            Check.NotNullOrWhiteSpace(noFactura, nameof(noFactura));
            Check.Length(noFactura, nameof(noFactura), ImporExportConsts.NoFacturaMaxLength);
            Check.Length(observaciones, nameof(observaciones), ImporExportConsts.ObservacionesMaxLength);

            var imporExport = await _imporExportRepository.GetAsync(id);

            imporExport.ImportadorId = importadorId;
            imporExport.ExportadorId = exportadorId;
            imporExport.ProductoId = productoId;
            imporExport.UnidadMedidaId = unidadMedidaId;
            imporExport.TipoEnvaseId = tipoEnvaseId;
            imporExport.PuertoEntradaId = puertoEntradaId;
            imporExport.PuertoSalidaId = puertoSalidaId;
            imporExport.PaisProcedenciaId = paisProcedenciaId;
            imporExport.PaisDestinoId = paisDestinoId;
            imporExport.PaisOrigenId = paisOrigenId;
            imporExport.AlmacenId = almacenId;
            imporExport.PermisoRenov = permisoRenov;
            imporExport.PermisoDe = permisoDe;
            imporExport.NoPermiso = noPermiso;
            imporExport.FechaEmision = fechaEmision;
            imporExport.FechaSolicitud = fechaSolicitud;
            imporExport.PesoNeto = pesoNeto;
            imporExport.PesoUnitario = pesoUnitario;
            imporExport.CantEnvvase = cantEnvvase;
            imporExport.NoFactura = noFactura;
            imporExport.Observaciones = observaciones;
            imporExport.EsRenovacion = esRenovacion;
            imporExport.Estado = estado;

            return await _imporExportRepository.UpdateAsync(imporExport);
        }

    }
}