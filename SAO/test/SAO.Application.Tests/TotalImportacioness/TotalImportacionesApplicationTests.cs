using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.TotalImportacioness
{
    public class TotalImportacionessAppServiceTests : SAOApplicationTestBase
    {
        private readonly ITotalImportacionessAppService _totalImportacionessAppService;
        private readonly IRepository<TotalImportaciones, Guid> _totalImportacionesRepository;

        public TotalImportacionessAppServiceTests()
        {
            _totalImportacionessAppService = GetRequiredService<ITotalImportacionessAppService>();
            _totalImportacionesRepository = GetRequiredService<IRepository<TotalImportaciones, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _totalImportacionessAppService.GetListAsync(new GetTotalImportacionessInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.TotalImportaciones.Id == Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047")).ShouldBe(true);
            result.Items.Any(x => x.TotalImportaciones.Id == Guid.Parse("749de578-9a02-4a3f-8811-fa8b6b75730f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _totalImportacionessAppService.GetAsync(Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TotalImportacionesCreateDto
            {
                Anio = 1092455866,
                CuotaAsignada = 1674292370,
                CuotaConsumida = 1754498598,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                TipoProductoId = Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"),
                AsraeId = 1
            };

            // Act
            var serviceResult = await _totalImportacionessAppService.CreateAsync(input);

            // Assert
            var result = await _totalImportacionesRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Anio.ShouldBe(1092455866);
            result.CuotaAsignada.ShouldBe(1674292370);
            result.CuotaConsumida.ShouldBe(1754498598);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TotalImportacionesUpdateDto()
            {
                Anio = 256841024,
                CuotaAsignada = 1273296599,
                CuotaConsumida = 556986801,
                ImportadorId = Guid.Parse("5ba62d29-2b7d-4f81-b985-290658f73cbf"),
                TipoProductoId = Guid.Parse("dd6e0f8b-5ee4-44ae-b44a-6d862e4e7a81"),
                AsraeId = 1
            };

            // Act
            var serviceResult = await _totalImportacionessAppService.UpdateAsync(Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"), input);

            // Assert
            var result = await _totalImportacionesRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Anio.ShouldBe(256841024);
            result.CuotaAsignada.ShouldBe(1273296599);
            result.CuotaConsumida.ShouldBe(556986801);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _totalImportacionessAppService.DeleteAsync(Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"));

            // Assert
            var result = await _totalImportacionesRepository.FindAsync(c => c.Id == Guid.Parse("a72937f3-827f-4ced-acb3-40dabbb13047"));

            result.ShouldBeNull();
        }
    }
}