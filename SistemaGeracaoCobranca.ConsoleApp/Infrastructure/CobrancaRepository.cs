using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Repositories;

namespace SistemaGeracaoCobranca.ConsoleApp.Infrastructure;

public class CobrancaRepository : ICobrancaRepository
{
    public List<Cobranca> Cobrancas { get; set; } = [];

    public void Adicionar(Cobranca cobranca)
    {
        Cobrancas.Add(cobranca);
    }

    public Cobranca? ObterCobrancaPorNumeroDocumento(string numeroDocumento)
    {
        return Cobrancas.FirstOrDefault(c => c.Cliente.Documento.Numero == numeroDocumento);
    }
}
