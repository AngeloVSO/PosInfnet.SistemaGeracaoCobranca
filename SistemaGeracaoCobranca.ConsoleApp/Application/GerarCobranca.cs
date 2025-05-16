using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Factories;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Repositories;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Services;

namespace SistemaGeracaoCobranca.ConsoleApp.Application;

public class GerarCobranca
{
    private readonly ICobrancaRepository _cobrancaRepository;
    private readonly CobrancaDomainService _cobrancaDomainService;

    public GerarCobranca(ICobrancaRepository cobrancaRepository, CobrancaDomainService cobrancaDomainService)
    {
        _cobrancaRepository = cobrancaRepository;
        _cobrancaDomainService = cobrancaDomainService;
    }

    public string ExecutarCobrancaPara(Cliente cliente, decimal valorCobranca, string? chavePixDestino = null)
    {
        Cobranca cobranca = CriarCobranca(cliente, chavePixDestino, valorCobranca);
        try
        {
            _cobrancaDomainService.ValidarSePodeGerar(cobranca);
            var resultadoCobrancaGerada = cobranca.Gerar();
            _cobrancaRepository.Adicionar(cobranca);
            return resultadoCobrancaGerada;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public Cobranca? ObterCobrancaPorNumeroDocumento(string numeroDocumento)
    {
        return _cobrancaRepository.ObterCobrancaPorNumeroDocumento(numeroDocumento);

    }

    private static Cobranca CriarCobranca(Cliente cliente, string? chavePixDestino, decimal valorCobranca)
    {
        if (string.IsNullOrEmpty(chavePixDestino))
        {
            return CobrancaFactory.Criar(valorCobranca, cliente);
        }
        
        return CobrancaFactory.Criar(valorCobranca, chavePixDestino, cliente);
    }
}
