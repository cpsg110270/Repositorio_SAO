using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.TipoEnvases
{
    public class TipoEnvasesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITipoEnvaseRepository _tipoEnvaseRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TipoEnvasesDataSeedContributor(ITipoEnvaseRepository tipoEnvaseRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _tipoEnvaseRepository = tipoEnvaseRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _tipoEnvaseRepository.InsertAsync(new TipoEnvase
            (
                desEnvase: "5cde84e5796f44f98a83"
            ));

            await _tipoEnvaseRepository.InsertAsync(new TipoEnvase
            (
                desEnvase: "da10ddf1795b46b6a3c0"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}