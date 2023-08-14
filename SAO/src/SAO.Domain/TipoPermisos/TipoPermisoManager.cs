using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SAO.TipoPermisos
{
    public class TipoPermisoManager : DomainService
    {
        private readonly ITipoPermisoRepository _tipoPermisoRepository;

        public TipoPermisoManager(ITipoPermisoRepository tipoPermisoRepository)
        {
            _tipoPermisoRepository = tipoPermisoRepository;
        }

        public async Task<TipoPermiso> CreateAsync(
        string codigo, string desripcion)
        {
            Check.NotNullOrWhiteSpace(codigo, nameof(codigo));
            Check.Length(codigo, nameof(codigo), TipoPermisoConsts.CodigoMaxLength, TipoPermisoConsts.CodigoMinLength);
            Check.Length(desripcion, nameof(desripcion), TipoPermisoConsts.DesripcionMaxLength);

            var tipoPermiso = new TipoPermiso(
             GuidGenerator.Create(),
             codigo, desripcion
             );

            return await _tipoPermisoRepository.InsertAsync(tipoPermiso);
        }

        public async Task<TipoPermiso> UpdateAsync(
            Guid id,
            string codigo, string desripcion
        )
        {
            Check.NotNullOrWhiteSpace(codigo, nameof(codigo));
            Check.Length(codigo, nameof(codigo), TipoPermisoConsts.CodigoMaxLength, TipoPermisoConsts.CodigoMinLength);
            Check.Length(desripcion, nameof(desripcion), TipoPermisoConsts.DesripcionMaxLength);

            var tipoPermiso = await _tipoPermisoRepository.GetAsync(id);

            tipoPermiso.Codigo = codigo;
            tipoPermiso.Desripcion = desripcion;

            return await _tipoPermisoRepository.UpdateAsync(tipoPermiso);
        }

    }
}