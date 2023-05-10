using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.Importadors
{
    public class ImportadorsAppServiceTests : SAOApplicationTestBase
    {
        private readonly IImportadorsAppService _importadorsAppService;
        private readonly IRepository<Importador, Guid> _importadorRepository;

        public ImportadorsAppServiceTests()
        {
            _importadorsAppService = GetRequiredService<IImportadorsAppService>();
            _importadorRepository = GetRequiredService<IRepository<Importador, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _importadorsAppService.GetListAsync(new GetImportadorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("356eac29-4fd5-494f-b442-597bfe47ceb2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _importadorsAppService.GetAsync(Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ImportadorCreateDto
            {
                NombreImportador = "9a5b0891ea7f46f2a1f57b06eb85d91dfd67e78ddf6440a2af5ea643fa5d2bea0fee204ddb1547e891710796c05519d58aa7f97701074055be2a47153ed7079fe86581dd465f4498ba4ca8a2b08bd9da5069ae51b6ec40d3bee1a3bb1add25faa0d4fe594ea947bc8e71e419afcfcc87265d699de4df47c9b552dc5b9b"
            };

            // Act
            var serviceResult = await _importadorsAppService.CreateAsync(input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreImportador.ShouldBe("9a5b0891ea7f46f2a1f57b06eb85d91dfd67e78ddf6440a2af5ea643fa5d2bea0fee204ddb1547e891710796c05519d58aa7f97701074055be2a47153ed7079fe86581dd465f4498ba4ca8a2b08bd9da5069ae51b6ec40d3bee1a3bb1add25faa0d4fe594ea947bc8e71e419afcfcc87265d699de4df47c9b552dc5b9b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ImportadorUpdateDto()
            {
                NombreImportador = "ba878452123043db99d0af2ed188c1c5d922e8b1b2664de99ee375aa8c0898a8cab0f83eaf4c4a0ea5f70aa4d87ee4cd3860dd1a68b94f05abdb998e512c697a2170e4eb82634f9596211fde11707f16ef319e0b9c804b0d8f0dac9ea2a04540b2fbb62cd5d64eefa25af6b0e321e25fd66a18cc4e334dafbd99a16022"
            };

            // Act
            var serviceResult = await _importadorsAppService.UpdateAsync(Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"), input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreImportador.ShouldBe("ba878452123043db99d0af2ed188c1c5d922e8b1b2664de99ee375aa8c0898a8cab0f83eaf4c4a0ea5f70aa4d87ee4cd3860dd1a68b94f05abdb998e512c697a2170e4eb82634f9596211fde11707f16ef319e0b9c804b0d8f0dac9ea2a04540b2fbb62cd5d64eefa25af6b0e321e25fd66a18cc4e334dafbd99a16022");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _importadorsAppService.DeleteAsync(Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"));

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == Guid.Parse("7a33d145-8971-4bbf-aa7f-72ffbf6d0e37"));

            result.ShouldBeNull();
        }
    }
}