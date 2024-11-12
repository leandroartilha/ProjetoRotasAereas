namespace Application.UseCase.Rotas.Criar
{
    public interface ICriarRotaUseCase
    {
        public bool CriarRota(CriarRotaInputModel input, CancellationToken cancellationToken);
    }
}
