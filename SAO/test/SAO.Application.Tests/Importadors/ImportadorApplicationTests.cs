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
            result.Items.Any(x => x.Id == Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8b1e0137-3086-4b35-8eff-5fe1c0316bb4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _importadorsAppService.GetAsync(Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ImportadorCreateDto
            {
                NoImportador = 313593074,
                NoRUC = "335f03ba9ffa4371a036",
                NombreImportador = "57fc41faeee8468b81b3dd0bf48a9b7ab129b059931340d4adb96ebf204eb144a57325e97c8f4359b30197546f38bd139d85d1e870844aad86e29abf7a28162b9141d43966144062a5b5561e73978380761bbbc6d14b453995d5ca46d7a1f06636e50b34d7394b40924aac78e322f30aa7f31ec4bc7547edbcb1ac2ad6"
            };

            // Act
            var serviceResult = await _importadorsAppService.CreateAsync(input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoImportador.ShouldBe(313593074);
            result.NoRUC.ShouldBe("335f03ba9ffa4371a036");
            result.NombreImportador.ShouldBe("57fc41faeee8468b81b3dd0bf48a9b7ab129b059931340d4adb96ebf204eb144a57325e97c8f4359b30197546f38bd139d85d1e870844aad86e29abf7a28162b9141d43966144062a5b5561e73978380761bbbc6d14b453995d5ca46d7a1f06636e50b34d7394b40924aac78e322f30aa7f31ec4bc7547edbcb1ac2ad6");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ImportadorUpdateDto()
            {
                NoImportador = 1830042013,
                NoRUC = "61d4e0ad0857471f8b10",
                NombreImportador = "52e3060847e142299c4df1ac946f88eb055d945e8b38469e9cb6d589c4b7ceecddf19540b2e84135bb518161848ce18c909a12c45f3a443481642783047ef5d047c456fb7ad2408c870e5821258e6f8efe070bc62fef4d6483c70e562fb85b68a21e92d05abd4836a3b8dfff7baa9d55b126945ff48b4ef690dfa614a6"
            };

            // Act
            var serviceResult = await _importadorsAppService.UpdateAsync(Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"), input);

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NoImportador.ShouldBe(1830042013);
            result.NoRUC.ShouldBe("61d4e0ad0857471f8b10");
            result.NombreImportador.ShouldBe("52e3060847e142299c4df1ac946f88eb055d945e8b38469e9cb6d589c4b7ceecddf19540b2e84135bb518161848ce18c909a12c45f3a443481642783047ef5d047c456fb7ad2408c870e5821258e6f8efe070bc62fef4d6483c70e562fb85b68a21e92d05abd4836a3b8dfff7baa9d55b126945ff48b4ef690dfa614a6");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _importadorsAppService.DeleteAsync(Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"));

            // Assert
            var result = await _importadorRepository.FindAsync(c => c.Id == Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"));

            result.ShouldBeNull();
        }
    }
}