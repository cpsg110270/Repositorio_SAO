using SAO.TipoProductos;
using SAO.Asraes;
using SAO.Importadors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.CuotaImportadors;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICuotaImportadorRepository _cuotaImportadorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ImportadorsDataSeedContributor _importadorsDataSeedContributor;

        private readonly AsraesDataSeedContributor _asraesDataSeedContributor;

        private readonly TipoProductosDataSeedContributor _tipoProductosDataSeedContributor;

        public CuotaImportadorsDataSeedContributor(ICuotaImportadorRepository cuotaImportadorRepository, IUnitOfWorkManager unitOfWorkManager, ImportadorsDataSeedContributor importadorsDataSeedContributor, AsraesDataSeedContributor asraesDataSeedContributor, TipoProductosDataSeedContributor tipoProductosDataSeedContributor)
        {
            _cuotaImportadorRepository = cuotaImportadorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _importadorsDataSeedContributor = importadorsDataSeedContributor; _asraesDataSeedContributor = asraesDataSeedContributor; _tipoProductosDataSeedContributor = tipoProductosDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorsDataSeedContributor.SeedAsync(context);
            await _asraesDataSeedContributor.SeedAsync(context);
            await _tipoProductosDataSeedContributor.SeedAsync(context);

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("9f819c31-81ec-4116-aa71-d7d06f0f2b55"),
                año: 1330701843,
                cuota: 855624032,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: null,
                tipoProductoId: null
            ));

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("052ee185-f673-4df4-9e35-9135d5dca813"),
                año: 1291140032,
                cuota: 1046780911,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: null,
                tipoProductoId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}