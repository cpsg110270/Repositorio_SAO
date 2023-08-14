using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.Fabricantes
{
    public class FabricantesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IFabricanteRepository _fabricanteRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public FabricantesDataSeedContributor(IFabricanteRepository fabricanteRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _fabricanteRepository = fabricanteRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _fabricanteRepository.InsertAsync(new Fabricante
            (
                id: Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"),
                nombreFabricante: "3ab80e7fa9184a89bacbf94deea86287a5456483798b4e27ba495a67e9c72637779f872aabaf45fcac21a1c3607908959991"
            ));

            await _fabricanteRepository.InsertAsync(new Fabricante
            (
                id: Guid.Parse("e7392780-d2de-440c-ac61-0a981febb445"),
                nombreFabricante: "eb6863cedc7348868b080664f404d9d6b4727a32b6a3408ca4a2117e048c8d67487fa08d86354ac3b4dc952dc4812c19e7e3"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}