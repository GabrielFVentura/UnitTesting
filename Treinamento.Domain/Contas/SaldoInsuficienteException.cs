using System;

namespace Treinamento.Domain.Contas
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException(double saldo)
            : base($"Não há saldo suficiente na conta. Seu saldo atual é de R$ { saldo }")
        {
        }
    }
}