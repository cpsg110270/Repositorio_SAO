using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.Asraes;

namespace SAO.Asraes
{
    public class AsraesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAsraeRepository _asraeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AsraesDataSeedContributor(IAsraeRepository asraeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _asraeRepository = asraeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _asraeRepository.InsertAsync(new Asrae
            (
                codigo_ASHRAE: "a098480ecddc",
                descripcion: "2546aa9855c246d3bf0633f12dc0d63"
            ));

            await _asraeRepository.InsertAsync(new Asrae
            (
                codigo_ASHRAE: "94d95c88de5a",
                descripcion: "ed943729fe55465995fa44a34f4918d6e25cac8f08d7479cbadf1ee3c4d875d0faf93bb48f4443"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}