namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Factories;

public static class CobrancaFactory
{
    public static Cobranca Criar(decimal valor, Cliente cliente)
    {
        return new CobrancaBoleto { Valor = valor, Cliente = cliente };
    }

    public static Cobranca Criar(decimal valor, string chavePixDestino, Cliente cliente)
    {
        var cobrancaPix = new CobrancaPix { Valor = valor, Cliente = cliente };
        cobrancaPix.GerarCodigoPix(chavePixDestino);
        return cobrancaPix;
    }
}
