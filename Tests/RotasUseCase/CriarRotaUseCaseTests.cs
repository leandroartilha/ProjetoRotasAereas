using Moq;
using Domain.Entities;
using Domain.RepositoriesInterfaces;
using Application.UseCase.Rotas.Criar;

namespace Tests.RotasUseCase
{
    public class CriarRotaUseCaseTests : IClassFixture<RotaFixture>
    {
        private readonly CriarRotaUseCase _criarRotaUseCase;
        private readonly Mock<IRotaRepository> _rotaRepositoryMock;
        private readonly RotaFixture _fixture;

        public CriarRotaUseCaseTests(RotaFixture fixture)
        {
            _fixture = fixture;
            _rotaRepositoryMock = new Mock<IRotaRepository>();
            _criarRotaUseCase = new CriarRotaUseCase(_rotaRepositoryMock.Object);
        }

        [Fact]
        public async Task CriarRota_DeveCriarRotaComSucesso()
        {
            //Arrange
            var input = _fixture.GetCriarRotaInput();

            //Act
            var result = await _criarRotaUseCase.CriarRota(input, new CancellationToken());

            //Assert
            _rotaRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Rota>(), new CancellationToken()), Times.Once);
            Assert.True(result);
        }
    }

    public class RotaFixture
    {
        public CriarRotaInputModel GetCriarRotaInput()
        {
            return new CriarRotaInputModel
            {
                Origem = "GRU",
                Destino = "CDG",
                Valor = 100,
                Paradas = new List<string> { "BRC", "SCL" }
            };
        }
    }
}
