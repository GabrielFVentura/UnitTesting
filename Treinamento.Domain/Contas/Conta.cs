using System;
using System.Collections.Generic;
using System.Linq;

namespace Treinamento.Domain.Contas
{
    public class Conta : IAggregateRoot
    {
        public Conta()
        {
            this.Numero = CriarNumeroDaConta();
            this._movimentacoes = new List<IContaMovimentacao>();
            this.Ativa = true;
        }
        
        public string Numero { get; private set; }
        public bool Ativa { get; private set; }

        public bool getAtiva()
        {
            return Ativa;
        }

        public bool checkAtiva()
        {
            if (getAtiva() == false)
            {
                throw new ContaInativaException();
            }

            return Ativa;
        }

        public void changeAtiva(bool status)
        {
            this.Ativa = status;
            
        }

        private readonly IList<IContaMovimentacao> _movimentacoes;
        public IReadOnlyList<IContaMovimentacao> Movimentacoes => _movimentacoes.ToList();

        public void Depositar(double valor)
        {
            if (getAtiva() == false)
            {
                throw new ContaInativaException();
            }
            if (valor < 0)
            {
                throw new SaldoNegativoOuZeroException();
            }
            this._movimentacoes.Add(new Deposito(valor));  
        } 

        public void Sacar(double valor)
        {
            var saldo = this.ObterSaldo();
            if (valor < 5)
            {
                throw new SaqueMinException();
            }
            
            if (valor > 1000)
            {
                throw new SaqueMaxException();
            }
            
            if (getAtiva() == false)
            {
                throw new ContaInativaException();
            }
            if (saldo < valor)
            {
                throw new SaldoInsuficienteException(saldo);
            }

            this._movimentacoes.Add(new Saque(valor));
        }

        public double ObterSaldo() => this.CalcularTotalDeDepositos() - this.CalcularTotalDeSaques();

        private double CalcularTotalDeDepositos()
        {
            var total = 0.0;

            foreach (var movimentacao in this._movimentacoes)
            {
                if (movimentacao.Tipo == TipoMovimentacao.Deposito)
                {
                    total = total + movimentacao.Valor;
                }
            }

            return total;
        }

        private double CalcularTotalDeSaques()
        {
            var total = 0.0;

            foreach (var movimentacao in this._movimentacoes)
            {
                if (movimentacao.Tipo == TipoMovimentacao.Saqeue)
                {
                    total = total + movimentacao.Valor;
                }
            }

            return total;
        }

        private string CriarNumeroDaConta()
        {
            var numeroUnico = DateTime.Now.Millisecond;

            while (numeroUnico < 100)
            {
                numeroUnico = DateTime.Now.Millisecond;
            }
            return $"1234-{numeroUnico}";
        }
    }
}