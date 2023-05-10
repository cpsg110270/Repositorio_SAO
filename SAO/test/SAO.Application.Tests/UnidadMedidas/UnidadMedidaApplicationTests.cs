using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.UnidadMedidas
{
    public class UnidadMedidasAppServiceTests : SAOApplicationTestBase
    {
        private readonly IUnidadMedidasAppService _unidadMedidasAppService;
        private readonly IRepository<UnidadMedida, int> _unidadMedidaRepository;

        public UnidadMedidasAppServiceTests()
        {
            _unidadMedidasAppService = GetRequiredService<IUnidadMedidasAppService>();
            _unidadMedidaRepository = GetRequiredService<IRepository<UnidadMedida, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _unidadMedidasAppService.GetListAsync(new GetUnidadMedidasInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _unidadMedidasAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UnidadMedidaCreateDto
            {
                Abreviatura = "659b",
                NombreUnidad = "d2b64325dcee4a9ea575b78b0150c157cb4ea17ae3db4a9fad"
            };

            // Act
            var serviceResult = await _unidadMedidasAppService.CreateAsync(input);

            // Assert
            var result = await _unidadMedidaRepository.FindAsync(c => c.Abreviatura == serviceResult.Abreviatura);

            result.ShouldNotBe(null);
            result.Abreviatura.ShouldBe("659b");
            result.NombreUnidad.ShouldBe("d2b64325dcee4a9ea575b78b0150c157cb4ea17ae3db4a9fad");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UnidadMedidaUpdateDto()
            {
                Abreviatura = "ff78",
                NombreUnidad = "5b292bbc68904d829aac4c7d6f236d041dd585d7da064cdc93"
            };

            // Act
            var serviceResult = await _unidadMedidasAppService.UpdateAsync(1, input);

            // Assert
            var result = await _unidadMedidaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Abreviatura.ShouldBe("ff78");
            result.NombreUnidad.ShouldBe("5b292bbc68904d829aac4c7d6f236d041dd585d7da064cdc93");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _unidadMedidasAppService.DeleteAsync(1);

            // Assert
            var result = await _unidadMedidaRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}