using SAO.TipoPermisos;
using SAO.ImporExports;
using SAO.Almacens;
using SAO.Paiss;
using SAO.PuertoEntradaSalidas;
using SAO.TipoEnvases;
using SAO.UnidadMedidas;
using SAO.Productos;
using SAO.Exportadors;
using SAO.Importadors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.ImporExports;

namespace SAO.ImporExports
{
    public class ImporExportsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IImporExportRepository _imporExportRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ImportadorsDataSeedContributor _importadorsDataSeedContributor;

        private readonly ExportadorsDataSeedContributor _exportadorsDataSeedContributor;

        private readonly ProductosDataSeedContributor _productosDataSeedContributor;

        private readonly UnidadMedidasDataSeedContributor _unidadMedidasDataSeedContributor;

        private readonly TipoEnvasesDataSeedContributor _tipoEnvasesDataSeedContributor;

        private readonly PuertoEntradaSalidasDataSeedContributor _puertoEntradaSalidasDataSeedContributor;

        private readonly PaissDataSeedContributor _paissDataSeedContributor;

        private readonly AlmacensDataSeedContributor _almacensDataSeedContributor;

        private readonly ImporExportsDataSeedContributor _imporExportsDataSeedContributor;

        private readonly TipoPermisosDataSeedContributor _tipoPermisosDataSeedContributor;

        public ImporExportsDataSeedContributor(IImporExportRepository imporExportRepository, IUnitOfWorkManager unitOfWorkManager, ImportadorsDataSeedContributor importadorsDataSeedContributor, ExportadorsDataSeedContributor exportadorsDataSeedContributor, ProductosDataSeedContributor productosDataSeedContributor, UnidadMedidasDataSeedContributor unidadMedidasDataSeedContributor, TipoEnvasesDataSeedContributor tipoEnvasesDataSeedContributor, PuertoEntradaSalidasDataSeedContributor puertoEntradaSalidasDataSeedContributor, PaissDataSeedContributor paissDataSeedContributor, AlmacensDataSeedContributor almacensDataSeedContributor, ImporExportsDataSeedContributor imporExportsDataSeedContributor, TipoPermisosDataSeedContributor tipoPermisosDataSeedContributor)
        {
            _imporExportRepository = imporExportRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _importadorsDataSeedContributor = importadorsDataSeedContributor; _exportadorsDataSeedContributor = exportadorsDataSeedContributor; _productosDataSeedContributor = productosDataSeedContributor; _unidadMedidasDataSeedContributor = unidadMedidasDataSeedContributor; _tipoEnvasesDataSeedContributor = tipoEnvasesDataSeedContributor; _puertoEntradaSalidasDataSeedContributor = puertoEntradaSalidasDataSeedContributor; _paissDataSeedContributor = paissDataSeedContributor; _almacensDataSeedContributor = almacensDataSeedContributor; _imporExportsDataSeedContributor = imporExportsDataSeedContributor; _tipoPermisosDataSeedContributor = tipoPermisosDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorsDataSeedContributor.SeedAsync(context);
            await _exportadorsDataSeedContributor.SeedAsync(context);
            await _productosDataSeedContributor.SeedAsync(context);
            await _unidadMedidasDataSeedContributor.SeedAsync(context);
            await _tipoEnvasesDataSeedContributor.SeedAsync(context);
            await _puertoEntradaSalidasDataSeedContributor.SeedAsync(context);
            await _paissDataSeedContributor.SeedAsync(context);
            await _almacensDataSeedContributor.SeedAsync(context);
            await _imporExportsDataSeedContributor.SeedAsync(context);
            await _tipoPermisosDataSeedContributor.SeedAsync(context);

            await _imporExportRepository.InsertAsync(new ImporExport
            (
                id: Guid.Parse("b9680f28-6f29-4b16-aee6-1b5c9e3db1cb"),
                noPermiso: "fafd4ec1",
                fechaEmision: new DateTime(2018, 9, 4),
                fechaSolicitud: new DateTime(2008, 6, 2),
                pesoNeto: 1052513259,
                pesoUnitario: 1773907177,
                cantEnvvase: 1186787215,
                noFactura: "e79d2f518ff847f198b4d4c4cf863d",
                observaciones: "5b76b50ec1514eaa9264890e49aa7b3981055a5789694d7881844278f1a8a742d6c98518f6aa4123808cab7ad6a802614e2000efa8c84c38b5262faa2f893676e366d2bdcb01454497dd28ef5fd2295674dd4297cad94e75ba1d28accd678e3e4cc0dd71a704425c8c47402b8f0866bfbed647653b9f4360a2dd40c187c4b20b219f57e2f0bb474a8be6a4c122e8f4b9a12982064864",
                esRenovacion: true,
                estado: true,
                importadorId: null,
                exportadorId: Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                productoId: Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                unidadMedidaId: 1,
                tipoEnvaseId: 1,
                puertoEntradaId: null,
                puertoSalidaId: null,
                paisProcedenciaId: null,
                paisDestinoId: null,
                paisOrigenId: null,
                almacenId: null,
                permisoRenov: null,
                permisoDe: Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")
            ));

            await _imporExportRepository.InsertAsync(new ImporExport
            (
                id: Guid.Parse("6b5d90e7-4b57-48b2-b56a-3ccdaf0e4e13"),
                noPermiso: "cc29d75c",
                fechaEmision: new DateTime(2009, 6, 8),
                fechaSolicitud: new DateTime(2019, 1, 22),
                pesoNeto: 934289620,
                pesoUnitario: 164346702,
                cantEnvvase: 1336622671,
                noFactura: "e66c525e10934a36b27bdc16467d37",
                observaciones: "74e3fc89d0284cbcb9b560f3b0fd7c6d5b69fa378f224721a44ceba651dfe81703d2ba663fd140e59f1b277884787a3b751cb073d54248988d5e4e7bd24d3cd9fb02baeefcc94c59a73f03cc255be421a4216226398641218a0755947929513441e7b5a710e24f7aa9f1f8648d4234f0978e65b9e7534a1ab3f5af154e3c6e37a9e2021392ac447a855f408a545b30dcd63682bb01f5",
                esRenovacion: true,
                estado: true,
                importadorId: null,
                exportadorId: Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                productoId: Guid.Parse("456e2e3a-e84d-417d-9251-4cd05ad039d4"),
                unidadMedidaId: 2,
                tipoEnvaseId: 2,
                puertoEntradaId: null,
                puertoSalidaId: null,
                paisProcedenciaId: null,
                paisDestinoId: null,
                paisOrigenId: null,
                almacenId: null,
                permisoRenov: null,
                permisoDe: Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}