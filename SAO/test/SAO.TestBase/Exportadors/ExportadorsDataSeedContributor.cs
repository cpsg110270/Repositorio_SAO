using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.Exportadors
{
    public class ExportadorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IExportadorRepository _exportadorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ExportadorsDataSeedContributor(IExportadorRepository exportadorRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _exportadorRepository = exportadorRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _exportadorRepository.InsertAsync(new Exportador
            (
                id: Guid.Parse("1c1f1bcc-d95b-49e1-b7c9-befd55eab051"),
                nombreExportador: "fa05910eacc04eed84410c02e676d0129bafa3df0e7140c0becd87fc64c160edd80154cea9b4413faa73337085b289a4645dd38992f2409988c8ee7ef32ec4cb26995d37898a46dc94ef9b9b009ea7c7f7c805211e7f4aa38aa7b122fb24c0cc8ee01088af6e4b9db06386bdc9c822b8d9403a524478471c8912ef077e"
            ));

            await _exportadorRepository.InsertAsync(new Exportador
            (
                id: Guid.Parse("3ce1ea30-6071-4a85-a84e-6a2074fd3373"),
                nombreExportador: "ea2375fcec124d43bf6b9b3df17c07776b6b9856ab43419a986c6b99ae4e66e1bc447e3645664f9fbc66be9f7069cce58266e7d4f8a845aa8a11c7b9f6cacba76d9a71321be9411ca6e1263c544c1e1c3d04c4c8024b424d9aebc7ecccfa5e061a290e4b7c01450c8bdb507a154aff721b34e3d12f154a8aa1f52ff941"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}