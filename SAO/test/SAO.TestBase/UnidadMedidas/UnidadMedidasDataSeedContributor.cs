using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.UnidadMedidas;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidasDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUnidadMedidaRepository _unidadMedidaRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnidadMedidasDataSeedContributor(IUnidadMedidaRepository unidadMedidaRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _unidadMedidaRepository = unidadMedidaRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _unidadMedidaRepository.InsertAsync(new UnidadMedida
            (
                abreviatura: "6a70",
                nombreUnidad: "aca2b1a0e61e460ca807f83d479047794cef37699f4a417db8"
            ));

            await _unidadMedidaRepository.InsertAsync(new UnidadMedida
            (
                abreviatura: "d9da",
                nombreUnidad: "03c564243a6b4a6382a180f774e86d6134cdc0f064194208b9"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}