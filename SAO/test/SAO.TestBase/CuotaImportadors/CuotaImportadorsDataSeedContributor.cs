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

        public CuotaImportadorsDataSeedContributor(ICuotaImportadorRepository cuotaImportadorRepository, IUnitOfWorkManager unitOfWorkManager, ImportadorsDataSeedContributor importadorsDataSeedContributor)
        {
            _cuotaImportadorRepository = cuotaImportadorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _importadorsDataSeedContributor = importadorsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _importadorsDataSeedContributor.SeedAsync(context);

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("c4967a32-1e44-4e3d-9798-af87dd9e5c23"),
                año: 1053693814,
                cuota: 1462952506,
                importadorId: Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37")
            ));

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("b957dc8f-fc60-4d39-8296-07639568a9ae"),
                año: 765829526,
                cuota: 1203376057,
                importadorId: Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}