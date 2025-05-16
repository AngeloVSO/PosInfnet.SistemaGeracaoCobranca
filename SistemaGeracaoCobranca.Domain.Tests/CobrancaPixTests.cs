using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

namespace SistemaGeracaoCobranca.Domain.Tests;

public class CobrancaPixTests
{
    [Fact]
    public void Gerar_DeveRetornarMensagemCorreta_QuandoDadosValidos()
    {
        var clienteJoao = new Cliente("João", "CPF", "123.456.789-00");
        var cobranca = new CobrancaPix { Valor = 3500, Cliente = clienteJoao };
        cobranca.GerarCodigoPix(chavePixDestino: "joao@banco.com.br");

        var resultadoEsperado = $"Cobrança com código PIX {cobranca.CodigoPix} gerado para {cobranca.Cliente.Nome}, valor: R${cobranca.Valor}, data de expiração {cobranca.DataExpiracao}.";

        var resultado = cobranca.Gerar();

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void Gerar_DeveLancarExcecao_QuandoCodigoPixInvalido()
    {
        var clienteMaria = new Cliente("Maria", "CPF", "987.654.321-00");
        var cobranca = new CobrancaPix
        {
            Valor = 2000,
            Cliente = clienteMaria,
            CodigoPix = "maria@banco.com"
        };

        var mensagemEsperada = $"Cobrança rejeitada: código pix {cobranca.CodigoPix} inválido.";

        var excecao = Assert.Throws<Exception>(() => cobranca.Gerar());

        Assert.Equal(mensagemEsperada, excecao.Message);
    }

    [Fact]
    public void Gerar_DeveLancarExcecao_QuandoDataExpiracaoMenorQueDataCriacao()
    {
        var clienteCarlos = new Cliente("Carlos", "CPF", "555.666.777-88");
        var cobranca = new CobrancaPix
        {
            Valor = 1200,
            Cliente = clienteCarlos,
            DataExpiracao = DateTime.Now.AddDays(-1)
        };
        cobranca.GerarCodigoPix("carlos@empresa.com");

        var mensagemEsperada = $"Cobrança rejeitada: código pix {cobranca.CodigoPix} inválido.";

        var excecao = Assert.Throws<Exception>(() => cobranca.Gerar());

        Assert.Equal(mensagemEsperada, excecao.Message);
    }
}
