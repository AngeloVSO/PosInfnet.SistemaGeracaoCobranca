namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

public class CobrancaBoleto : Cobranca
{
    public string CodigoBarras { get; set; } = GerarCodigoDeBarras();
    public DateTime DataVencimento { get; set; } = DateTime.Now.AddDays(5);

    const int quantidadeDigitos = 13;

    public override string Gerar()
    {
        if (DataVencimento < DataCriacao)
        {
            throw new Exception($"Cobrança rejeitada: data vencimento do boleto menor que data da geração.");
        }

        if (CodigoBarras.Length < quantidadeDigitos)
        {
            throw new Exception($"Cobrança rejeitada: código de barras com quantidade de dígitos incorreto.");
        }

        return $"Cobrança via Boleto com código de barras {CodigoBarras} gerado para {Cliente.Nome}, valor: R${Valor}, data de vencimento {DataVencimento}.";
    }

    private static string GerarCodigoDeBarras()
    {
        Random rnd = new Random();
        string codigo = "";

        for (int i = 0; i < quantidadeDigitos; i++)
        {
            codigo += rnd.Next(0, 10);
        }

        return codigo;
    }
}
