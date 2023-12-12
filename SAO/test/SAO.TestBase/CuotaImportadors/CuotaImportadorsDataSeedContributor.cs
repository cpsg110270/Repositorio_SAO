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
                id: Guid.Parse("5c6dbff7-f7eb-4b3d-a718-a76b73def62c"),
                año: 1666648358,
                cuota: 1252537514,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: 1
            ));

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("298e7061-ab4f-4c2c-a631-122a8e5157fb"),
                año: 1380996054,
                cuota: 865915050,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                asraeId: 2
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}