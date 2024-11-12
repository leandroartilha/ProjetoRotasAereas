namespace Application.UseCase.Rotas.Criar
{
    public class CriarRotaInputModel
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double Valor { get; set; }
        public List<string>? Paradas { get; set; }
    }
}
