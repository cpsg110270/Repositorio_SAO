using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.TipoPermisos
{
    public class TipoPermisosAppServiceTests : SAOApplicationTestBase
    {
        private readonly ITipoPermisosAppService _tipoPermisosAppService;
        private readonly IRepository<TipoPermiso, Guid> _tipoPermisoRepository;

        public TipoPermisosAppServiceTests()
        {
            _tipoPermisosAppService = GetRequiredService<ITipoPermisosAppService>();
            _tipoPermisoRepository = GetRequiredService<IRepository<TipoPermiso, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _tipoPermisosAppService.GetListAsync(new GetTipoPermisosInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("0e9246c7-f7fb-4d3c-8c0c-cf11c57d3265")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _tipoPermisosAppService.GetAsync(Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TipoPermisoCreateDto
            {
                Codigo = "f21",
                Desripcion = "58e001789d7d422a8d81"
            };

            // Act
            var serviceResult = await _tipoPermisosAppService.CreateAsync(input);

            // Assert
            var result = await _tipoPermisoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Codigo.ShouldBe("f21");
            result.Desripcion.ShouldBe("58e001789d7d422a8d81");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TipoPermisoUpdateDto()
            {
                Codigo = "e27",
                Desripcion = "2f491f5769d34bc2a79b"
            };

            // Act
            var serviceResult = await _tipoPermisosAppService.UpdateAsync(Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"), input);

            // Assert
            var result = await _tipoPermisoRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Codigo.ShouldBe("e27");
            result.Desripcion.ShouldBe("2f491f5769d34bc2a79b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _tipoPermisosAppService.DeleteAsync(Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"));

            // Assert
            var result = await _tipoPermisoRepository.FindAsync(c => c.Id == Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"));

            result.ShouldBeNull();
        }
    }
}