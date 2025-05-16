using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.ValueObjects;

namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

public class Cliente
{
    public string Nome { get; private set; }
    public Documento Documento { get; private set; }
    public List<DadosPagamentoCobranca> DadosPagamentoCobranca { get; private set; } = [];

    public Cliente(string nome, string tipoDocumento, string numeroDocumento)
    {
        Nome = nome;
        Documento = new Documento(tipoDocumento, numeroDocumento);
    }

    public Cliente()
    {
    }

    public string ObterTipoDocumento()
    {
        return Documento.Tipo;
    }

    public string ObterNumeroDocumento()
    {
        return Documento.Numero;
    }

    public void AdicionarDadosPagamentoCobranca(string dadoPagamentoCobranca)
    {
        var chavePix = new DadosPagamentoCobranca(dadoPagamentoCobranca);
        DadosPagamentoCobranca.Add(chavePix);
    }

    public string ObterCahvePixDadosPagamentoCobranca()
    {
        var chavePix = DadosPagamentoCobranca.FirstOrDefault(x => x.ChavePix != null)?.ChavePix;
        return chavePix;
    }
}
