using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using SAO.Almacens;

namespace SAO.Almacens
{
    public class AlmacensDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAlmacenRepository _almacenRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AlmacensDataSeedContributor(IAlmacenRepository almacenRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _almacenRepository = almacenRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _almacenRepository.InsertAsync(new Almacen
            (
                nombreAlmacen: "850c92993e914bcc8af3970d4ff7294b8f39e37d8a7c44ec9106b9f9472fdcd4d2ffcc8373b64125aa7fd99da08c7ce33d8f346ca43145879c7845e52a628a13bccd40950ce24b79aa7c2ea5879a65ea97cea73b4942469a8eca94a0d1be09c512d5e7ce",
                siglaAlmacen: "d17332e18e3a43d38349"
            ));

            await _almacenRepository.InsertAsync(new Almacen
            (
                nombreAlmacen: "93ca62d65a224cd98b5960dfbcde354420b87208f33f46bf8beeb2f0798cef2af27afe22248e49bebe033227c4376a89c1fcced9fffd434289c73984833bbaf367940d87596a4df89280a6abc628f22f84f428313b8d48849bdb32b8af321315f67341af",
                siglaAlmacen: "5bcfcf302ddb4c119bab"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}