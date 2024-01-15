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

        public CuotaImportadorsDataSeedContributor(ICuotaImportadorRepository cuotaImportadorRepository, IUnitOfWorkManager unitOfWorkManager, ImportadorsDataSeedContributor importadorsDataSeedContributor, AsraesDataSeedContributor asraesDataSeedContributor)
        {
            _cuotaImportadorRepository = cuotaImportadorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _importadorsDataSeedContributor = importadorsDataSeedContributor; _asraesDataSeedContributor = asraesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorsDataSeedContributor.SeedAsync(context);
            await _asraesDataSeedContributor.SeedAsync(context);

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("9792ab87-5e58-4884-b7ac-642ab57a3303"),
                año: 1505516303,
                cuota: 1512334946,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: null
            ));

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("46fe4e12-301c-4aa7-84c3-81a4e4cbaaa1"),
                año: 548327134,
                cuota: 29385357,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}