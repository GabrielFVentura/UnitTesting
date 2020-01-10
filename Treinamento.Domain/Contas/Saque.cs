namespace Treinamento.Domain.Contas
{
    public class Saque : IContaMovimentacao
    {
        public Saque(double valor)
        {
            this.Tipo = TipoMovimentacao.Saqeue;
            this.Valor = valor;
        }

        public double Valor { get; private set;  }
        public TipoMovimentacao Tipo { get; private set; }
    }
}