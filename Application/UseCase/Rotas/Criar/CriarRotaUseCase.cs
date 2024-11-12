using Domain.Entities;
using Domain.RepositoriesInterfaces;

namespace Application.UseCase.Rotas.Criar
{
    public class CriarRotaUseCase
    {
        private readonly IRotaRepository _rotaRepository;

        public CriarRotaUseCase(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public async Task<bool> CriarRota(CriarRotaInputModel input, CancellationToken cancellationToken)
        {
            var rota = new Rota()
            {
                Origem = input.Origem,
                Destino = input.Destino,
                Valor = input.Valor,
                Paradas = input.Paradas
            };

            await _rotaRepository.CreateAsync(rota, cancellationToken);

            return true;
        }
    }
}
