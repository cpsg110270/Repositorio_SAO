using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.Paiss
{
    public class PaissDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPaisRepository _paisRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PaissDataSeedContributor(IPaisRepository paisRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _paisRepository = paisRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _paisRepository.InsertAsync(new Pais
            (
                nombrePais: "63a276cd0b3148a692b0b239731c44beb9570a3c44f24ea49a"
            ));

            await _paisRepository.InsertAsync(new Pais
            (
                nombrePais: "3451766268c24073af5802f915e398fd2f9c6eb67b014b12a3"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}