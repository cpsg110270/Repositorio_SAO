using SAO.Asraes;
using SAO.TipoProductos;
using SAO.Importadors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.TotalImportacioness;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionessDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITotalImportacionesRepository _totalImportacionesRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ImportadorsDataSeedContributor _importadorsDataSeedContributor;

        private readonly TipoProductosDataSeedContributor _tipoProductosDataSeedContributor;

        private readonly AsraesDataSeedContributor _asraesDataSeedContributor;

        public TotalImportacionessDataSeedContributor(ITotalImportacionesRepository totalImportacionesRepository, IUnitOfWorkManager unitOfWorkManager, ImportadorsDataSeedContributor importadorsDataSeedContributor, TipoProductosDataSeedContributor tipoProductosDataSeedContributor, AsraesDataSeedContributor asraesDataSeedContributor)
        {
            _totalImportacionesRepository = totalImportacionesRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _importadorsDataSeedContributor = importadorsDataSeedContributor; _tipoProductosDataSeedContributor = tipoProductosDataSeedContributor; _asraesDataSeedContributor = asraesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorsDataSeedContributor.SeedAsync(context);
            await _tipoProductosDataSeedContributor.SeedAsync(context);
            await _asraesDataSeedContributor.SeedAsync(context);

            await _totalImportacionesRepository.InsertAsync(new TotalImportaciones
            (
                id: Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"),
                anio: 1863721905,
                cuotaAsignada: 714479628,
                cuotaConsumida: 698887346,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                tipoProductoId: Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"),
                asraeId: 1
            ));

            await _totalImportacionesRepository.InsertAsync(new TotalImportaciones
            (
                id: Guid.Parse("749de578-9a02-4a3f-8811-fa8b6b75730f"),
                anio: 1956626758,
                cuotaAsignada: 714886421,
                cuotaConsumida: 189183780,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                tipoProductoId: Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"),
                asraeId: 2
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}