using SistemaGeracaoCobranca.ConsoleApp.Application;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model;
using SistemaGeracaoCobranca.ConsoleApp.Domain.Model.Services;
using SistemaGeracaoCobranca.ConsoleApp.Infrastructure;

Console.WriteLine("Bem vindo ao sistema de geração de cobrança!");

var cobrancaRepository = new CobrancaRepository();
var cobrancaDomainService = new CobrancaDomainService(cobrancaRepository);
var service = new GerarCobranca(cobrancaRepository, cobrancaDomainService);

var joaoComCPF = new Cliente("João", "CPF", "123.456.789-00");
var cobrancaParaJoao = service.ExecutarCobrancaPara(cliente: joaoComCPF, valorCobranca: 4500);
Console.WriteLine(cobrancaParaJoao);

var empresaXComCNPJ = new Cliente("Empresa X", "CNPJ", "12.345.678/0001-99");
var cobrancaParaEmpresaX = service.ExecutarCobrancaPara(cliente: empresaXComCNPJ, valorCobranca: 10000);
Console.WriteLine(cobrancaParaEmpresaX);

var clienteJoseComCPF = new Cliente("José", "CPF", "123.456.000-11");
joaoComCPF.AdicionarDadosPagamentoCobranca("123.456.000-11");

var cobrancaParaJosePagarJoao = service.ExecutarCobrancaPara(cliente: clienteJoseComCPF, valorCobranca: 5000, chavePixDestino: joaoComCPF.ObterCahvePixDadosPagamentoCobranca());
Console.WriteLine(cobrancaParaJosePagarJoao);

var cobrancaJoaoValor4500Obtida = service.ObterCobrancaPorNumeroDocumento(joaoComCPF.ObterNumeroDocumento());
var cobrancaParaJosePagarJoaoObtida = service.ObterCobrancaPorNumeroDocumento(clienteJoseComCPF.ObterNumeroDocumento());

Console.WriteLine($"Dados cobrança de {cobrancaJoaoValor4500Obtida?.Cliente.Nome}");
ImprimirResultado(cobrancaJoaoValor4500Obtida);

Console.WriteLine($"Dados cobrança de {cobrancaParaJosePagarJoaoObtida?.Cliente.Nome}");
ImprimirResultado(cobrancaParaJosePagarJoaoObtida);


// Teste de cobrança acima do limite para CPF
var mariaComCPF = new Cliente("Maria", "CPF", "333.444.555-66");
service.ExecutarCobrancaPara(cliente: mariaComCPF, valorCobranca: 5500);

static void ImprimirResultado(Cobranca cobranca)
{
    if (cobranca is CobrancaBoleto cobrancaBoleto)
    {
        Console.WriteLine($@"
    ID: {cobrancaBoleto?.Id}
    Nome Cliente: {cobrancaBoleto?.Cliente.Nome}
    Valor Cobrado: {cobrancaBoleto?.Valor}
    Data Cobranca: {cobrancaBoleto?.DataCriacao}
    Data Vencimento: {cobrancaBoleto?.DataVencimento}
    Códgigo de Barras: {cobrancaBoleto?.CodigoBarras}");
    }
    else if (cobranca is CobrancaPix cobrancaPix)
    {
        Console.WriteLine($@"
    ID: {cobrancaPix?.Id}
    Nome Cliente: {cobrancaPix?.Cliente.Nome}
    Valor Cobrado: {cobrancaPix?.Valor}
    Data Cobranca: {cobrancaPix?.DataCriacao}
    Data Vencimento: {cobrancaPix?.DataExpiracao}
    Código PIX: {cobrancaPix?.CodigoPix}");
    }
}

Console.ReadLine();