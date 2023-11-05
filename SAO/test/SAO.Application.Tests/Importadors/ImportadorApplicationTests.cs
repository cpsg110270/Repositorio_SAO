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
            result.Items.Any(x => x.Id == Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("dc15f0ac-c05a-4df1-a379-2c730f920eb2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _importadorsAppService.GetAsync(Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ImportadorCreateDto
            {
                NombreImportador = "4f8dee8418f2490aa04952206bc7c8646ba946d7fc7b49f8874504078369f4e47a8c4dd4d7a84439856f774737753d7485868dc4ebfa431894f0fec81d9c3025d0d2076460ee454b811d2a47372ab41d547e702ae3524914955780990a4d451a7d7762772da34e1ea4f4a8e7a0d1ad314747d470c25e4f87913934098e",
                NoRUC = "73f1815de4fc41eba82c"
            };

            // Act
            var serviceResult = await _importadorsAppService.CreateAsync(input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreImportador.ShouldBe("4f8dee8418f2490aa04952206bc7c8646ba946d7fc7b49f8874504078369f4e47a8c4dd4d7a84439856f774737753d7485868dc4ebfa431894f0fec81d9c3025d0d2076460ee454b811d2a47372ab41d547e702ae3524914955780990a4d451a7d7762772da34e1ea4f4a8e7a0d1ad314747d470c25e4f87913934098e");
            result.NoRUC.ShouldBe("73f1815de4fc41eba82c");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ImportadorUpdateDto()
            {
                NombreImportador = "22dc0f3e58e04acbb5892b53d9d32416678e3bedc3534039af42c2dd74aa5c8c6f735a53c0f741acbac24e934028fd0d856b2748fe6b4a7387cc2c482fda59a46d4e641dfe254fc480be9322fdd05c12a17732852f6f4922b37c46d89091739753de41ae4d274373803ba8e07dfe2379e7e8c071225b4a45b92e147a6f",
                NoRUC = "680ac1785ebe442db596"
            };

            // Act
            var serviceResult = await _importadorsAppService.UpdateAsync(Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"), input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombreImportador.ShouldBe("22dc0f3e58e04acbb5892b53d9d32416678e3bedc3534039af42c2dd74aa5c8c6f735a53c0f741acbac24e934028fd0d856b2748fe6b4a7387cc2c482fda59a46d4e641dfe254fc480be9322fdd05c12a17732852f6f4922b37c46d89091739753de41ae4d274373803ba8e07dfe2379e7e8c071225b4a45b92e147a6f");
            result.NoRUC.ShouldBe("680ac1785ebe442db596");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _importadorsAppService.DeleteAsync(Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"));

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == Guid.Parse("7f4bc753-28f7-474a-a2d3-90ce35f5dbe0"));

            result.ShouldBeNull();
        }
    }
}