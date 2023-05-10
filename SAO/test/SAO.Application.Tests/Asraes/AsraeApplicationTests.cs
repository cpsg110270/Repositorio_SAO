using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Asraes
{
    public class AsraesAppServiceTests : SAOApplicationTestBase
    {
        private readonly IAsraesAppService _asraesAppService;
        private readonly IRepository<Asrae, int> _asraeRepository;

        public AsraesAppServiceTests()
        {
            _asraesAppService = GetRequiredService<IAsraesAppService>();
            _asraeRepository = GetRequiredService<IRepository<Asrae, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _asraesAppService.GetListAsync(new GetAsraesInput());

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
            var result = await _asraesAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AsraeCreateDto
            {
                Codigo_ASHRAE = "272550a2860e",
                Descripcion = "f722c1d9054948"
            };

            // Act
            var serviceResult = await _asraesAppService.CreateAsync(input);

            // Assert
            var result = await _asraeRepository.FindAsync(c => c.Codigo_ASHRAE == serviceResult.Codigo_ASHRAE);

            result.ShouldNotBe(null);
            result.Codigo_ASHRAE.ShouldBe("272550a2860e");
            result.Descripcion.ShouldBe("f722c1d9054948");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AsraeUpdateDto()
            {
                Codigo_ASHRAE = "deaa044d403b",
                Descripcion = "8a490858654e402"
            };

            // Act
            var serviceResult = await _asraesAppService.UpdateAsync(1, input);

            // Assert
            var result = await _asraeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Codigo_ASHRAE.ShouldBe("deaa044d403b");
            result.Descripcion.ShouldBe("8a490858654e402");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _asraesAppService.DeleteAsync(1);

            // Assert
            var result = await _asraeRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}