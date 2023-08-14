using SAO.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SAO.TipoPermisos
{
    public class TipoPermisoRepositoryTests : SAOEntityFrameworkCoreTestBase
    {
        private readonly ITipoPermisoRepository _tipoPermisoRepository;

        public TipoPermisoRepositoryTests()
        {
            _tipoPermisoRepository = GetRequiredService<ITipoPermisoRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _tipoPermisoRepository.GetListAsync(
                    codigo: "d20",
                    desripcion: "cf2ad2be0bb74578b39b"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("ed75d956-eec7-46a9-af86-cc1f32dedbf2"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _tipoPermisoRepository.GetCountAsync(
                    codigo: "ba5",
                    desripcion: "532eb783ecb04fe69020"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}