using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SAO.ImporExports
{
    public interface IImporExportRepository : IRepository<ImporExport, Guid>
    {
        Task<ImporExportWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<ImporExportWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            Guid? importadorId = null,
            Guid? exportadorId = null,
            Guid? productoId = null,
            int? unidadMedidaId = null,
            int? tipoEnvaseId = null,
            int? puertoEntradaId = null,
            int? puertoSalidaId = null,
            int? paisProcedenciaId = null,
            int? paisDestinoId = null,
            int? paisOrigenId = null,
            int? almacenId = null,
            Guid? permisoRenov = null,
            Guid? permisoDe = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<ImporExport>> GetListAsync(
                    string filterText = null,
                    string noPermiso = null,
                    DateTime? fechaEmisionMin = null,
                    DateTime? fechaEmisionMax = null,
                    DateTime? fechaSolicitudMin = null,
                    DateTime? fechaSolicitudMax = null,
                    double? pesoNetoMin = null,
                    double? pesoNetoMax = null,
                    double? pesoUnitarioMin = null,
                    double? pesoUnitarioMax = null,
                    int? cantEnvvaseMin = null,
                    int? cantEnvvaseMax = null,
                    string noFactura = null,
                    string observaciones = null,
                    bool? esRenovacion = null,
                    bool? estado = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string noPermiso = null,
            DateTime? fechaEmisionMin = null,
            DateTime? fechaEmisionMax = null,
            DateTime? fechaSolicitudMin = null,
            DateTime? fechaSolicitudMax = null,
            double? pesoNetoMin = null,
            double? pesoNetoMax = null,
            double? pesoUnitarioMin = null,
            double? pesoUnitarioMax = null,
            int? cantEnvvaseMin = null,
            int? cantEnvvaseMax = null,
            string noFactura = null,
            string observaciones = null,
            bool? esRenovacion = null,
            bool? estado = null,
            Guid? importadorId = null,
            Guid? exportadorId = null,
            Guid? productoId = null,
            int? unidadMedidaId = null,
            int? tipoEnvaseId = null,
            int? puertoEntradaId = null,
            int? puertoSalidaId = null,
            int? paisProcedenciaId = null,
            int? paisDestinoId = null,
            int? paisOrigenId = null,
            int? almacenId = null,
            Guid? permisoRenov = null,
            Guid? permisoDe = null,
            CancellationToken cancellationToken = default);
    }
}