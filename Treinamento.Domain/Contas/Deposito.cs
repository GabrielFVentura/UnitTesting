namespace Treinamento.Domain.Contas
{
    public class Deposito : IContaMovimentacao
    {
        public Deposito(double valor)
        {
            this.Tipo = TipoMovimentacao.Deposito;
            this.Valor = valor;
        }
        
        public double Valor { get; private set;  }
        public TipoMovimentacao Tipo { get; private set;  }
        
    }
}