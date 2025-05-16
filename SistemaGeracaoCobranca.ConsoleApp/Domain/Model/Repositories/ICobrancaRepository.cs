namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Repositories;

public interface ICobrancaRepository
{
    void Adicionar(Cobranca cobranca);
    Cobranca? ObterCobrancaPorNumeroDocumento(string numeroDocumento);
}
