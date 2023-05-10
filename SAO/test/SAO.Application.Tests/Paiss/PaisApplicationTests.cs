using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Paiss
{
    public class PaissAppServiceTests : SAOApplicationTestBase
    {
        private readonly IPaissAppService _paissAppService;
        private readonly IRepository<Pais, int> _paisRepository;

        public PaissAppServiceTests()
        {
            _paissAppService = GetRequiredService<IPaissAppService>();
            _paisRepository = GetRequiredService<IRepository<Pais, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _paissAppService.GetListAsync(new GetPaissInput());

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
            var result = await _paissAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PaisCreateDto
            {
                NombrePais = "3c3f17db8b78496b90238f70d30368c4c250d547bc974456a9"
            };

            // Act
            var serviceResult = await _paissAppService.CreateAsync(input);

            // Assert
            var result = await _paisRepository.FindAsync(c => c.NombrePais == serviceResult.NombrePais);

            result.ShouldNotBe(null);
            result.NombrePais.ShouldBe("3c3f17db8b78496b90238f70d30368c4c250d547bc974456a9");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PaisUpdateDto()
            {
                NombrePais = "4f8b842cee514af6a29d874967675de6a4153d73170545ffa3"
            };

            // Act
            var serviceResult = await _paissAppService.UpdateAsync(1, input);

            // Assert
            var result = await _paisRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombrePais.ShouldBe("4f8b842cee514af6a29d874967675de6a4153d73170545ffa3");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _paissAppService.DeleteAsync(1);

            // Assert
            var result = await _paisRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}