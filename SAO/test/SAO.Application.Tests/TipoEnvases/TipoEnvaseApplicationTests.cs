using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.TipoEnvases
{
    public class TipoEnvasesAppServiceTests : SAOApplicationTestBase
    {
        private readonly ITipoEnvasesAppService _tipoEnvasesAppService;
        private readonly IRepository<TipoEnvase, int> _tipoEnvaseRepository;

        public TipoEnvasesAppServiceTests()
        {
            _tipoEnvasesAppService = GetRequiredService<ITipoEnvasesAppService>();
            _tipoEnvaseRepository = GetRequiredService<IRepository<TipoEnvase, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _tipoEnvasesAppService.GetListAsync(new GetTipoEnvasesInput());

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
            var result = await _tipoEnvasesAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TipoEnvaseCreateDto
            {
                DesEnvase = "a56914dc78b84157bcde"
            };

            // Act
            var serviceResult = await _tipoEnvasesAppService.CreateAsync(input);

            // Assert
            var result = await _tipoEnvaseRepository.FindAsync(c => c.DesEnvase == serviceResult.DesEnvase);

            result.ShouldNotBe(null);
            result.DesEnvase.ShouldBe("a56914dc78b84157bcde");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TipoEnvaseUpdateDto()
            {
                DesEnvase = "68f8bd2cb6be4294b6e7"
            };

            // Act
            var serviceResult = await _tipoEnvasesAppService.UpdateAsync(1, input);

            // Assert
            var result = await _tipoEnvaseRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DesEnvase.ShouldBe("68f8bd2cb6be4294b6e7");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _tipoEnvasesAppService.DeleteAsync(1);

            // Assert
            var result = await _tipoEnvaseRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}