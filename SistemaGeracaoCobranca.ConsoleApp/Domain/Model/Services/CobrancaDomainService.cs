using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Repositories;

namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Services;

public class CobrancaDomainService : ICobrancaDomainService
{
    private readonly ICobrancaRepository _cobrancaRepository;
    public CobrancaDomainService(ICobrancaRepository cobrancaRepository)
    {
        _cobrancaRepository = cobrancaRepository;
    }

    public void ValidarSePodeGerar(Cobranca cobranca)
    {
        decimal limiteValorPagamentoPorCPF = 5000;

        if (cobranca.Cliente.ObterTipoDocumento() == "CPF" && cobranca.Valor > limiteValorPagamentoPorCPF)
        {
            throw new Exception("Cobrança para cliente com tipo documento igual a CPF não pode ser maior que 5000.");
        }

        var cobrancaObtida = _cobrancaRepository.ObterCobrancaPorNumeroDocumento(cobranca.Cliente.ObterNumeroDocumento());

        if (cobrancaObtida != null)
        {
            throw new Exception("Já existe uma cobrança para este cliente.");
        }
    }
}