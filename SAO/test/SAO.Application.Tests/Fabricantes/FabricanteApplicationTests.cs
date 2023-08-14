using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Fabricantes
{
    public class FabricantesAppServiceTests : SAOApplicationTestBase
    {
        private readonly IFabricantesAppService _fabricantesAppService;
        private readonly IRepository<Fabricante, Guid> _fabricanteRepository;

        public FabricantesAppServiceTests()
        {
            _fabricantesAppService = GetRequiredService<IFabricantesAppService>();
            _fabricanteRepository = GetRequiredService<IRepository<Fabricante, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _fabricantesAppService.GetListAsync(new GetFabricantesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("e7392780-d2de-440c-ac61-0a981febb445")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _fabricantesAppService.GetAsync(Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new FabricanteCreateDto
            {
                NombreFabricante = "c41c16172c9f4188aeaa193817dbfedba088654e433a4c7891bf39aaa99bf75bac7b76dad5c14f2c821046f8351376b44dd2"
            };

            // Act
            var serviceResult = await _fabricantesAppService.CreateAsync(input);

            // Assert
            var result = await _fabricanteRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreFabricante.ShouldBe("c41c16172c9f4188aeaa193817dbfedba088654e433a4c7891bf39aaa99bf75bac7b76dad5c14f2c821046f8351376b44dd2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new FabricanteUpdateDto()
            {
                NombreFabricante = "130104f63f854f12bf5a48a9b1fec121b1e319c57a934cdfb0d21223e7c38b829a39bae849804da3b30bafefe25fdcf127f1"
            };

            // Act
            var serviceResult = await _fabricantesAppService.UpdateAsync(Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"), input);

            // Assert
            var result = await _fabricanteRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreFabricante.ShouldBe("130104f63f854f12bf5a48a9b1fec121b1e319c57a934cdfb0d21223e7c38b829a39bae849804da3b30bafefe25fdcf127f1");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _fabricantesAppService.DeleteAsync(Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"));

            // Assert
            var result = await _fabricanteRepository.FindAsync(c => c.Id == Guid.Parse("d4944455-6f12-40c8-b177-99ce52c6ac28"));

            result.ShouldBeNull();
        }
    }
}