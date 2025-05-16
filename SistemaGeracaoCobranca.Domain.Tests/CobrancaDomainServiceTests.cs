using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Services;
using SistemaGeracaoCobranca.ConsoleApp.Infrastructure;

namespace SistemaGeracaoCobranca.Domain.Tests;

public class CobrancaDomainServiceTests
{
    [Fact]
    public void ValidarSePodeGerar_DeveLancarExcecao_QuandoClienteCPFComValorAcimaDoPermitido()
    {
        var cliente = new Cliente("Ana", "CPF", "123.456.789-00");
        var cobranca = new CobrancaBoleto { Valor = 6000, Cliente = cliente };

        var repositorio = new CobrancaRepository();
        var service = new CobrancaDomainService(repositorio);

        var excecao = Assert.Throws<Exception>(() => service.ValidarSePodeGerar(cobranca));

        Assert.Equal("Cobrança para cliente com tipo documento igual a CPF não pode ser maior que 5000.", excecao.Message);
    }

    [Fact]
    public void ValidarSePodeGerar_DeveLancarExcecao_QuandoJaExisteCobrancaParaMesmoDocumento()
    {
        var cliente = new Cliente("Bruno", "CPF", "987.654.321-00");
        var cobrancaUm = new CobrancaBoleto { Valor = 1000, Cliente = cliente };
        var cobrancaExistente = new CobrancaBoleto { Valor = 300, Cliente = cliente };

        var repositorio = new CobrancaRepository();
        var service = new CobrancaDomainService(repositorio);

        repositorio.Adicionar(cobrancaUm);

        var excecao = Assert.Throws<Exception>(() => service.ValidarSePodeGerar(cobrancaExistente));

        Assert.Equal("Já existe uma cobrança para este cliente.", excecao.Message);
    }

    [Fact]
    public void ValidarSePodeGerar_NaoDeveLancarExcecao_QuandoDadosValidos()
    {
        var cliente = new Cliente("Carlos", "CPF", "555.666.777-88");
        var cobranca = new CobrancaBoleto { Valor = 4500, Cliente = cliente };

        var repositorio = new CobrancaRepository();
        var service = new CobrancaDomainService(repositorio);

        var exception = Record.Exception(() => service.ValidarSePodeGerar(cobranca));

        Assert.Null(exception);
    }
}
