using Application.UseCase.Rotas.BuscarMelhorRota;
using Application.UseCase.Rotas.Criar;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoRotasAereas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotasController : ControllerBase
    {
        private readonly ILogger<RotasController> _logger;
        private readonly ICriarRotaUseCase _criarRotaUseCase;
        private readonly IBuscarMelhorRotaUseCase _buscarMelhorRotaUseCase;

        public RotasController(ILogger<RotasController> logger, ICriarRotaUseCase criarRotaUseCase, IBuscarMelhorRotaUseCase buscarMelhorRotaUseCase)
        {
            _logger = logger;
            _criarRotaUseCase = criarRotaUseCase;
            _buscarMelhorRotaUseCase = buscarMelhorRotaUseCase;
        }

        [HttpPost(Name = "CriarRota")]
        public ActionResult<bool> CriarRota(CriarRotaInputModel input, CancellationToken cancellationToken)
        {
            try
            {
                var result = _criarRotaUseCase.CriarRota(input, cancellationToken);

                if (result)
                    return StatusCode(200);

                return BadRequest("Falha ao criar a rota. Verifique os dados enviados.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        [HttpGet(Name = "BuscarRotas")]
        public async Task<BuscarMelhorRotaViewModel> GetMelhorRota(BuscarMelhorRotaInputModel input, CancellationToken cancellationToken)
        {
            return await _buscarMelhorRotaUseCase.BuscarMelhorRota(input, cancellationToken);
        }
    }
}
