using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.Exportadors;

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
                id: Guid.Parse("64f96866-1a1b-41f1-a2d4-e0a690fc6a37"),
                noImportador: 2132260050,
                nombreExportador: "62da3494f29542a188664cce75a05cc02626e4ff87544c98aaa6d1e2ae236367172d6848a0c64d6eb2856c22fc5d53170e689ebda5fe44f08c9538964d7c7f82703c56e22b0a48ac959f2d183677ce6a80ceb60dc3d24e93b401d4788d52d14b2ed0bdb346de47b981b3b87e64a4ecda2fe42b9c9bf647aebd5cd6b01b"
            ));

            await _exportadorRepository.InsertAsync(new Exportador
            (
                id: Guid.Parse("edcb2c94-15f1-4d74-84e8-be72e1e6d87b"),
                noImportador: 1353836526,
                nombreExportador: "ec1f9e03820c4e82a9d5c2181864decba75c04e9736544f7a258f816bedf8a3526d35afd59154370a4e827543af437c36cf0ac7a4d8f449593537ce2e9700a59a5c5ba34d8874bb5917ff2ed43978339ad8c7f3db4a849ee99958cf6b4ef7fb3835143ea9eb94b8899ee51ac779d833f5e4148dc6d404017a970fcba4b"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}