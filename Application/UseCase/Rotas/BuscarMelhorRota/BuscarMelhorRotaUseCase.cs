using Domain.RepositoriesInterfaces;

namespace Application.UseCase.Rotas.BuscarMelhorRota
{
    public class BuscarMelhorRotaUseCase : IBuscarMelhorRotaUseCase
    {
        private readonly IRotaRepository _rotaRepository;

        public BuscarMelhorRotaUseCase(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }
        public async Task<BuscarMelhorRotaViewModel> BuscarMelhorRota(BuscarMelhorRotaInputModel input, CancellationToken cancellationToken)
        {
            var rotasDisponiveis = await _rotaRepository.GetAllAsync(cancellationToken);

            var rotasFiltradas = rotasDisponiveis.Where(x => x.Origem == input.Origem && x.Destino == input.Destino).ToList();

            if (!rotasFiltradas.Any())
            {
                return null;
            }

            var melhorRota = rotasFiltradas.Select(x =>
                {var valorTotal = x.Valor + (x.Paradas?.Count ?? 0) * 5;
                    return new
                    {
                        Rota = x,
                        ValorTotal = valorTotal
                    };
                }).OrderBy(r => r.ValorTotal).FirstOrDefault();

            if (melhorRota != null)
            {
                return new BuscarMelhorRotaViewModel
                {
                    Rota = $"{melhorRota.Rota.Origem} - {string.Join(" - ", melhorRota.Rota.Paradas)} - {melhorRota.Rota.Destino}",
                    Valor = melhorRota.ValorTotal
                };
            }

            return null;
        }
    }
}
