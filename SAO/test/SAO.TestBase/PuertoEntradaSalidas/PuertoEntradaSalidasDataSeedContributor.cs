using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidasDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPuertoEntradaSalidaRepository _puertoEntradaSalidaRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PuertoEntradaSalidasDataSeedContributor(IPuertoEntradaSalidaRepository puertoEntradaSalidaRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _puertoEntradaSalidaRepository = puertoEntradaSalidaRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _puertoEntradaSalidaRepository.InsertAsync(new PuertoEntradaSalida
            (
                nombrePuerto: "96d2471158d141e6952ade9d64c8e1fae6d92e97a9254fb58e"
            ));

            await _puertoEntradaSalidaRepository.InsertAsync(new PuertoEntradaSalida
            (
                nombrePuerto: "8a7eb6b486384d4c9db845e8a6010853e6fac99ceb074dcfad"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}