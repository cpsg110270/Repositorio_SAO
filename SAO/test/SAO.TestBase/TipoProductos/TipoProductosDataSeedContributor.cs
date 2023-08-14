using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.TipoProductos
{
    public class TipoProductosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITipoProductoRepository _tipoProductoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TipoProductosDataSeedContributor(ITipoProductoRepository tipoProductoRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _tipoProductoRepository = tipoProductoRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _tipoProductoRepository.InsertAsync(new TipoProducto
            (
                id: Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"),
                desProducto: "f504d2854c814153b194"
            ));

            await _tipoProductoRepository.InsertAsync(new TipoProducto
            (
                id: Guid.Parse("b6732dd0-cd26-40aa-ba08-a784f000255f"),
                desProducto: "f0cd8fc1bf68494c8168"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}