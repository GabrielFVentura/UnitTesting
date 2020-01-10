using System;

namespace Treinamento.Domain.Contas
{
    public class SaqueMaxException : Exception
    {
        public SaqueMaxException()
            : base($"O valor máximo permitido para saques é R$1000,00")
        {
        }
    }
}