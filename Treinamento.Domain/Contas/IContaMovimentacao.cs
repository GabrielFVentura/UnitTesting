namespace Treinamento.Domain.Contas
{
    public interface IContaMovimentacao
    {
        double Valor { get;  }
        TipoMovimentacao Tipo { get; }
    }
}