using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.SustanciaElementals
{
    public class SustanciaElementalsAppServiceTests : SAOApplicationTestBase
    {
        private readonly ISustanciaElementalsAppService _sustanciaElementalsAppService;
        private readonly IRepository<SustanciaElemental, Guid> _sustanciaElementalRepository;

        public SustanciaElementalsAppServiceTests()
        {
            _sustanciaElementalsAppService = GetRequiredService<ISustanciaElementalsAppService>();
            _sustanciaElementalRepository = GetRequiredService<IRepository<SustanciaElemental, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _sustanciaElementalsAppService.GetListAsync(new GetSustanciaElementalsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("23ccffe9-2f48-4e34-ac05-d26509f7eb09")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _sustanciaElementalsAppService.GetAsync(Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new SustanciaElementalCreateDto
            {
                CodCas = "716731980b9d4c7",
                DesSustancia = "89589599baa741d8a0be33a362b730d374a60e39bd6c4286ba"
            };

            // Act
            var serviceResult = await _sustanciaElementalsAppService.CreateAsync(input);

            // Assert
            var result = await _sustanciaElementalRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CodCas.ShouldBe("716731980b9d4c7");
            result.DesSustancia.ShouldBe("89589599baa741d8a0be33a362b730d374a60e39bd6c4286ba");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new SustanciaElementalUpdateDto()
            {
                CodCas = "880a7e0033134e6",
                DesSustancia = "6b2c58de4e77419c8b0fbdd433168ba16df720a04676451c99"
            };

            // Act
            var serviceResult = await _sustanciaElementalsAppService.UpdateAsync(Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"), input);

            // Assert
            var result = await _sustanciaElementalRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CodCas.ShouldBe("880a7e0033134e6");
            result.DesSustancia.ShouldBe("6b2c58de4e77419c8b0fbdd433168ba16df720a04676451c99");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _sustanciaElementalsAppService.DeleteAsync(Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"));

            // Assert
            var result = await _sustanciaElementalRepository.FindAsync(c => c.Id == Guid.Parse("abb9f38f-02b3-4c25-9703-9a928f83c185"));

            result.ShouldBeNull();
        }
    }
}