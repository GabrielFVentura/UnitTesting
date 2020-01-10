using System;

namespace Treinamento.Domain.Contas
    {
        public class SaldoNegativoOuZeroException : Exception
        {
            public SaldoNegativoOuZeroException()
                : base($"O valor do depósito não pode ser zero ou negativo")
            {
            }
        }
    }