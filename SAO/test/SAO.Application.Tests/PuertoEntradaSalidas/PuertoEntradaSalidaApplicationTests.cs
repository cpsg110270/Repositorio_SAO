using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace SAO.PuertoEntradaSalidas
{
    public class PuertoEntradaSalidasAppServiceTests : SAOApplicationTestBase
    {
        private readonly IPuertoEntradaSalidasAppService _puertoEntradaSalidasAppService;
        private readonly IRepository<PuertoEntradaSalida, int> _puertoEntradaSalidaRepository;

        public PuertoEntradaSalidasAppServiceTests()
        {
            _puertoEntradaSalidasAppService = GetRequiredService<IPuertoEntradaSalidasAppService>();
            _puertoEntradaSalidaRepository = GetRequiredService<IRepository<PuertoEntradaSalida, int>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _puertoEntradaSalidasAppService.GetListAsync(new GetPuertoEntradaSalidasInput());

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
            var result = await _puertoEntradaSalidasAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new PuertoEntradaSalidaCreateDto
            {
                NombrePuerto = "e95544e6eaab4851bd4e42aade58908081219c40aac24d9fa3"
            };

            // Act
            var serviceResult = await _puertoEntradaSalidasAppService.CreateAsync(input);

            // Assert
            var result = await _puertoEntradaSalidaRepository.FindAsync(c => c.NombrePuerto == serviceResult.NombrePuerto);

            result.ShouldNotBe(null);
            result.NombrePuerto.ShouldBe("e95544e6eaab4851bd4e42aade58908081219c40aac24d9fa3");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new PuertoEntradaSalidaUpdateDto()
            {
                NombrePuerto = "fe56818220434ca6a03bf5e262b90a629643cd4692124c489e"
            };

            // Act
            var serviceResult = await _puertoEntradaSalidasAppService.UpdateAsync(1, input);

            // Assert
            var result = await _puertoEntradaSalidaRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.NombrePuerto.ShouldBe("fe56818220434ca6a03bf5e262b90a629643cd4692124c489e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _puertoEntradaSalidasAppService.DeleteAsync(1);

            // Assert
            var result = await _puertoEntradaSalidaRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}