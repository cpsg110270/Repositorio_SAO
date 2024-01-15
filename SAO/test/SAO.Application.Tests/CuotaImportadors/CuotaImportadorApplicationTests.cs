using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.CuotaImportadors
{
    public class CuotaImportadorsAppServiceTests : SAOApplicationTestBase
    {
        private readonly ICuotaImportadorsAppService _cuotaImportadorsAppService;
        private readonly IRepository<CuotaImportador, Guid> _cuotaImportadorRepository;

        public CuotaImportadorsAppServiceTests()
        {
            _cuotaImportadorsAppService = GetRequiredService<ICuotaImportadorsAppService>();
            _cuotaImportadorRepository = GetRequiredService<IRepository<CuotaImportador, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetListAsync(new GetCuotaImportadorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1")).ShouldBe(true);
            result.Items.Any(x => x.CuotaImportador.Id == Guid.Parse("fa080ef6-e159-4a0c-838b-9190d30092a7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _cuotaImportadorsAppService.GetAsync(Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new CuotaImportadorCreateDto
            {
                Año = 1957684780,
                Cuota = 1214897360,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf")
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.CreateAsync(input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(1957684780);
            result.Cuota.ShouldBe(1214897360);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new CuotaImportadorUpdateDto()
            {
                Año = 1507602845,
                Cuota = 663668842,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf")
            };

            // Act
            var serviceResult = await _cuotaImportadorsAppService.UpdateAsync(Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"), input);

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Año.ShouldBe(1507602845);
            result.Cuota.ShouldBe(663668842);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _cuotaImportadorsAppService.DeleteAsync(Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"));

            // Assert
            var result = await _cuotaImportadorRepository.FindAsync(c => c.Id == Guid.Parse("bdc70f50-4bfd-423d-8018-8ad9995035b1"));

            result.ShouldBeNull();
        }
    }
}