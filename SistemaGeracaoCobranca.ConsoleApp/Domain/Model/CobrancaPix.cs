namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

public class CobrancaPix : Cobranca
{
    public string CodigoPix { get; set; }
    public DateTime DataExpiracao { get; set; } = DateTime.Now.AddDays(1);

    const string codigoBancoCentral = "br.gov.bcb.pix";

    public override string Gerar()
    {
        if (!CodigoPix.Contains(codigoBancoCentral))
        {
            throw new Exception($"Cobrança rejeitada: código pix {CodigoPix} inválido.");
        }

        if (DataExpiracao < DataCriacao)
        {
            throw new Exception($"Cobrança rejeitada: código pix {CodigoPix} inválido.");
        }

        return $"Cobrança com código PIX {CodigoPix} gerado para {Cliente.Nome}, valor: R${Valor}, data de expiração {DataExpiracao}.";
    }

    public void GerarCodigoPix(string chavePixDestino)
    {
        CodigoPix = $"{chavePixDestino}-{codigoBancoCentral}";
    }
}
