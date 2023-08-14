using SAO.Almacens;
using SAO.Exportadors;
using SAO.Importadors;
using SAO.Paiss;
using SAO.Productos;
using SAO.PuertoEntradaSalidas;
using SAO.TipoEnvases;
using SAO.TipoPermisos;
using SAO.UnidadMedidas;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

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
                id: Guid.Parse("a1bdbd90-a5ae-42f6-842f-395a52b70947"),
                noPermiso: "6919c449",
                fechaEmision: new DateTime(2007, 5, 10),
                fechaSolicitud: new DateTime(2018, 9, 8),
                pesoNeto: 911620457,
                pesoUnitario: 2044930474,
                cantEnvvase: 147070886,
                noFactura: "379a278d08de",
                observaciones: "c702a1fdd4bb44aeb6554727d879a01f75d2741e8cd24136abc4c0e00e775e566c0e2b9c2f4e4232ae9c34853108aa44eaceec067885402a8b4fe5084bbf59fa6e35f32f8b35469faf901705698f97467f9000fe68644cdaab38197321d9020305f3bd1061684605b03f56ad1054632d741ff67d9df0487fab7811d85d1f4ec53dd822593bef423c9ded06f281ab6f1ce8cb89acff5a",
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
                id: Guid.Parse("6faa3da5-873e-4002-a197-233d029685e8"),
                noPermiso: "d948eafb",
                fechaEmision: new DateTime(2005, 11, 3),
                fechaSolicitud: new DateTime(2001, 8, 3),
                pesoNeto: 1423904740,
                pesoUnitario: 630052434,
                cantEnvvase: 556033453,
                noFactura: "cebd4eedf3f0",
                observaciones: "7ac236c14bac4c3baf18cf5ec541105cc42428c0927b42eb9200dc57f596a99c1cbfa30516f3409f871367546ffd41213bf563d4820f4fba976e8f88e4cdea91ec5c93a3cdc1447b84d80e58120f41490d74aeacca76494986a03b13111ecca5c5f6991015334ba4b5fbd3fbadca05ef8584ecf0b85c4c61bbe611f91afe852f8be26d3bd5184b63a554f48b0c9213513169d3bf81be",
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