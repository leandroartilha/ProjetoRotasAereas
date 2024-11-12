namespace Application.UseCase.Rotas.BuscarMelhorRota
{
    public interface IBuscarMelhorRotaUseCase
    {
        public Task<BuscarMelhorRotaViewModel> BuscarMelhorRota(BuscarMelhorRotaInputModel input, CancellationToken cancellationToken);
    }
}
