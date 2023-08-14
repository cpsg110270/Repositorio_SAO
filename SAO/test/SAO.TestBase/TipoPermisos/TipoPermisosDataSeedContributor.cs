using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace SAO.TipoPermisos
{
    public class TipoPermisosDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITipoPermisoRepository _tipoPermisoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TipoPermisosDataSeedContributor(ITipoPermisoRepository tipoPermisoRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _tipoPermisoRepository = tipoPermisoRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _tipoPermisoRepository.InsertAsync(new TipoPermiso
            (
                id: Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"),
                codigo: "d20",
                desripcion: "cf2ad2be0bb74578b39b"
            ));

            await _tipoPermisoRepository.InsertAsync(new TipoPermiso
            (
                id: Guid.Parse("0e9246c7-f7fb-4d3c-8c0c-cf11c57d3265"),
                codigo: "ba5",
                desripcion: "532eb783ecb04fe69020"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}