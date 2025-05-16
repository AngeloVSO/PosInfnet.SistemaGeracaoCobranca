namespace SistemaGeracaoCobranca.ConsoleApp.Domain.Model;

public abstract class Cobranca
{
    public Guid Id { get; protected set; } = Guid.NewGuid(); 
    public decimal Valor { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public Cliente Cliente { get; set; } = new();

    public abstract string Gerar();
}
