using System;

namespace Treinamento.Domain.Contas
{
    public class ContaInativaException : Exception
    {
        public ContaInativaException()
            : base($"Não é possível realizar a operação. A conta está inativa")
        {
        }
    }
}