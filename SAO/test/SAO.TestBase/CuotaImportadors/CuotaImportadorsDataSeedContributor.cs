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
                id: Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"),
                año: 766170516,
                cuota: 108058313,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf")
            ));

            await _cuotaImportadorRepository.InsertAsync(new CuotaImportador
            (
                id: Guid.Parse("fa080ef6-e159-4a0c-838b-9190d30092a7"),
                año: 2065945524,
                cuota: 668582100,
                importadorId: Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}