using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

namespace SistemaGeracaoCobranca.Domain.Tests;

public class CobrancaBoletoTests
{
    [Fact]
    public void Gerar_DeveRetornarMensagemCorreta_QuandoDadosValidos()
    {
        var clienteJoao = new Cliente("Jo�o", "CPF", "123.456.789-00"); ;
        var cobranca = new CobrancaBoleto { Valor = 5000, Cliente = clienteJoao };

        var resultadoEsperado = $"Cobran�a via Boleto com c�digo de barras {cobranca.CodigoBarras} gerado para {cobranca.Cliente.Nome}, valor: R${cobranca.Valor}, data de vencimento {cobranca.DataVencimento}.";

        var resultado = cobranca.Gerar();

        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void Gerar_DeveLancarExcecao_QuandoDataVencimentoMenorQueDataCriacao()
    {
        var clienteMaria = new Cliente("Maria", "CPF", "987.654.321-00");
        var cobranca = new CobrancaBoleto
        {
            Valor = 1000,
            Cliente = clienteMaria
        };

        cobranca.DataCriacao = cobranca.DataVencimento.AddDays(1);

        var mensagemEsperada = "Cobran�a rejeitada: data vencimento do boleto menor que data da gera��o.";

        var excecao = Assert.Throws<Exception>(() => cobranca.Gerar());

        Assert.Equal(mensagemEsperada, excecao.Message);
    }

    [Fact]
    public void Gerar_DeveLancarExcecao_QuandoCodigoBarrasComQuantidadeIncorreta()
    {
        var clientePedro = new Cliente("Pedro", "CPF", "111.222.333-44");
        var cobranca = new CobrancaBoleto
        {
            Valor = 750,
            Cliente = clientePedro,
            CodigoBarras = "123"
        };

        var mensagemEsperada = "Cobran�a rejeitada: c�digo de barras com quantidade de d�gitos incorreto.";

        var excecao = Assert.Throws<Exception>(() => cobranca.Gerar());

        Assert.Equal(mensagemEsperada, excecao.Message);
    }
}