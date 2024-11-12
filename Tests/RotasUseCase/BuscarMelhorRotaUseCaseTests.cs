using Moq;
using Domain.Entities;
using Domain.RepositoriesInterfaces;
using Application.UseCase.Rotas.BuscarMelhorRota;

namespace Tests.RotasUseCase
{
    public class BuscarMelhorRotaUseCaseTests
    {
        private readonly Mock<IRotaRepository> _rotaRepositoryMock;
        private readonly BuscarMelhorRotaUseCase _buscarMelhorRotaUseCase;

        public BuscarMelhorRotaUseCaseTests()
        {
            _rotaRepositoryMock = new Mock<IRotaRepository>();
            _buscarMelhorRotaUseCase = new BuscarMelhorRotaUseCase(_rotaRepositoryMock.Object);
        }

        [Fact]
        public async Task BuscarMelhorRota_DeveRetornarMelhorRotaComCustoMenor()
        {
            //Arrange
            var rotasDisponiveis = new List<Rota>
            {
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 75, Paradas = new List<string> { "BRC", "SCL" } },
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 50, Paradas = new List<string> { "BRC" } },
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 60, Paradas = new List<string> { "SCL" } }
            };

            _rotaRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(rotasDisponiveis);

            var input = new BuscarMelhorRotaInputModel { Origem = "GRU", Destino = "CDG" };
            var cancellationToken = new CancellationToken();

            //Act
            var result = await _buscarMelhorRotaUseCase.BuscarMelhorRota(input, cancellationToken);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("GRU - BRC - CDG", result.Rota);
            Assert.Equal(55, result.Valor);
        }

        [Fact]
        public async Task BuscarMelhorRota_DeveRetornarNullQuandoNaoHouverRota()
        {
            //Arrange
            var rotasDisponiveis = new List<Rota>();
            _rotaRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(rotasDisponiveis);

            var input = new BuscarMelhorRotaInputModel { Origem = "GRU", Destino = "CDG" };
            var cancellationToken = new CancellationToken();

            //Act
            var result = await _buscarMelhorRotaUseCase.BuscarMelhorRota(input, cancellationToken);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BuscarMelhorRota_DeveRetornarRotaComParadas()
        {
            //Arrange
            var rotasDisponiveis = new List<Rota>
            {
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 75, Paradas = new List<string> { "BRC", "SCL" } },
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 60, Paradas = new List<string> { "SCL" } }
            };

            _rotaRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(rotasDisponiveis);

            var input = new BuscarMelhorRotaInputModel { Origem = "GRU", Destino = "CDG" };
            var cancellationToken = new CancellationToken();

            //Act
            var result = await _buscarMelhorRotaUseCase.BuscarMelhorRota(input, cancellationToken);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("GRU - SCL - CDG", result.Rota);
            Assert.Equal(65, result.Valor);
        }
    }
}
