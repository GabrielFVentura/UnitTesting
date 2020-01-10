using System;

namespace Treinamento.Domain.Contas
{
    public class SaqueMinException : Exception
    {
        public SaqueMinException()
            : base($"O valor mínimo permitido para saques é R$5,00")
        {
        }
    }
}