using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISustanciaElementalRepository _sustanciaElementalRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SustanciaElementalsDataSeedContributor(ISustanciaElementalRepository sustanciaElementalRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _sustanciaElementalRepository = sustanciaElementalRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _sustanciaElementalRepository.InsertAsync(new SustanciaElemental
            (
                id: Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"),
                codCas: "e2456395d55640d",
                desSustancia: "8f03f7799d1a46baa7b4a8b466615d24e72465d9969e481191"
            ));

            await _sustanciaElementalRepository.InsertAsync(new SustanciaElemental
            (
                id: Guid.Parse("23ccffe9-2f48-4e34-ac05-d26509f7eb09"),
                codCas: "e597dcd8b8d041b",
                desSustancia: "6e9cab24f0774173894ab1c4c442b7d0f090912a1ba54bd595"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}