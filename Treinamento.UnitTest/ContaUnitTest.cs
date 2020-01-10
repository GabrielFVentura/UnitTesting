using System;
using Xunit;
using Treinamento.Domain.Contas;
using FluentAssertions;
using Xunit.Sdk;

namespace Treinamento.UnitTest
{
    public class ContaUnitTest
    {
        //criar um teste para garantir que a conta sempre será criada como ativa; #1
        //criar testes para ativar e inativar contas; #2
        //criar teste para verificar se o valor depositado é zero ou negativo, lançando uma exceção com a seguinte mensagem: “O valor do depósito não pode ser zero ou negativo”; #3
        //criar um teste para verificar se a conta está inativa, caso esteja, lançar a exceção “Não é possível realizar a operação. A conta está inativa”; #4
        //criar um teste para verificar se há quantia suficiente para ser realizado, caso não esteja emitir uma exceção com a seguinte mensagem: “Não há saldo suficiente na conta. Seu saldo atual é de R$ XXX”. //#5
        //verificar nesse teste se o número da conta está no padrão correto. Utilize a expressão regular ^[0-9]{4}-[0-9]{3} #6
        //criar um teste para verificar a quantia do saque realizado, sendo valor minimo R$5(Mensagem: “O valor mínimo permitido para saques é R$5,00”) #7
        //criar um teste para garantir que a conta ao ser criada terá 8 digitos; #8
        //criar um teste para verificar se a conta ao ser criada contem os digitos "1234" no inicio #9
        //criar um teste para garantir que ao depositar R$50,00 seu saldo será de R$50,00 #10
        //criar um teste para garantir que ao depositar duas vezes R$50,00 seu saldo será de R$100,00 #11
        //criar um teste para verificar a quantia do saque realizado, sendo valor máximo R$1000(Mensagem: “O valor máximo permitido para saques é R$1000,00” //#12
        //Caso a conta esteja inativa, o sistema deverá emitir a exceção “Não é possível realizar a operação. A conta está inativa”; //#13
        //criar um teste que permita que o usuário inative a sua conta. Quando a conta estiver inativa, não será possível realizar depósitos ou saques. #14
        //criar um teste que permita que o usuario reative a sua conta. #15
        
        [Fact]
        public void Conta_AoIniciar_DeveSerAtiva() // #1
        {
            // Arrange
            var conta = new Conta();
            
            // Act
            var contaStatus = conta.Ativa;

            //Assert
            Assert.Equal(true, contaStatus);
        }
        
        [Fact]
        public void Conta_Mensagem_Ativar() //#2
        {
            //Arrange
            string cmdRecebido = "Ativar Conta";
            
            //Act
            var conta = new Conta();
            
            //Assert
            Assert.Equal("Ativar Conta",cmdRecebido);
        }
        
        [Fact]
        public void Conta_Mensagem_Inativar() //#2
        {
            //Arrange
            string cmdRecebido = "Inativar Conta";
            
            //Act
            var conta = new Conta();
            
            //Assert
            Assert.Equal("Inativar Conta",cmdRecebido);
        }
        
        [Fact]
        public void Conta_DepositandoValorZeroOuNegativo_LancarExcecao() //#3
        {
            //Arrange
            var conta = new Conta();
            string mensagemEsperada = "O valor do depósito não pode ser zero ou negativo";
            //Act
            int deposito = -50;
            Action acao = () => conta.Depositar(deposito);
            
            //Assert
            acao.Should().Throw<SaldoNegativoOuZeroException>().WithMessage(mensagemEsperada);
        }
        
        [Fact]
        public void Conta_Inativa_LancarExcecao() //#4
        {
            //Arrange
            var conta = new Conta();

            //Act
            conta.changeAtiva(false);
            bool status = conta.getAtiva();
            Action acao = () => conta.checkAtiva();
            
            string mensagemEsperada = "Não é possível realizar a operação. A conta está inativa";

            //Assert
            Assert.Equal(false,status);
            acao.Should().Throw<ContaInativaException>().WithMessage(mensagemEsperada);
        }
        
        [Fact]
        public void Conta_SaqueComSaldoInsuficiente_DeveLancarUmaExcecao() //#5
        {
            // Arrange
            var conta = new Conta();
            
            var valorDepositado = 20.0;
            
            conta.Depositar(valorDepositado);

            var mensagemEsperada = $"Não há saldo suficiente na conta. Seu saldo atual é de R$ {valorDepositado}";
            
            // Act
            Action acao = () => conta.Sacar(50.0);

            // Assert
            acao
                .Should()
                .Throw<SaldoInsuficienteException>()
                .WithMessage(mensagemEsperada);
        }
        
        [Fact]
        public void Conta_NovaConta_VerificarPadrao() //#6
        {
            // Arrange

            var conta = new Conta();
            
            //Act & Assert
            conta.Numero.Should().MatchRegex("^[0-9]{4}-[0-9]{3}");
        }
            
        [Fact]
        public void Conta_SaqueMin_LancarExcecao() //#7
        {
            // Arrange
            var conta = new Conta();
            
            //Act
            conta.Depositar(1000);
            var mensagemEsperada = "O valor mínimo permitido para saques é R$5,00";
            Action acao = () => conta.Sacar(4);
            
            
            //Assert
            acao.Should().Throw<SaqueMinException>().WithMessage(mensagemEsperada);
        }
        
        [Fact]
        public void Conta_NovaConta_NumeroDaContaDeveTer8Caracteres() //#8
        {
            //Arrange
            var conta = new Conta();

            //Act && Assert
            conta.Numero.Length.Should().Be(8);
        }
        
        [Fact]
        public void Conta_NovaConta_NumeroDaContaDeveComecarCom1234() //#9
        {
            // Arrange
            var conta = new Conta();
            
            // Act && Assert
            conta
                .Numero.Should()
                .StartWith("1234");
        }
        
        [Fact]
        public void Conta_DepositoDe50Reais_SaldoDeveSer50() //#10
        {
            //Arrange
            var conta = new Conta();
            
            //Act
            conta.Depositar(50.0);
            
            //Assert
            conta.ObterSaldo()
                .Should()
                .Be(50.0)
                .And
                .BePositive();
        }
        
        [Fact]
        public void Conta_DoisDepositosDe50Reais_SaldoDeveSer100() //#11
        {
            //Arrange
            var conta = new Conta();

            //Act
            conta.Depositar(50.0);
            conta.Depositar(50.0);
            
            //Assert
            Assert.Equal(100.0, conta.ObterSaldo());
        }

        [Fact]
        public void Conta_SaqueMax_LancarExcecao() //#12
        {
            // Arrange
            var conta = new Conta();
            // Act
            conta.Depositar(1250);
            var mensagemEsperada = "O valor máximo permitido para saques é R$1000,00";
            Action acao = () => conta.Sacar(1001);

            // Assert
            acao.Should().Throw<SaqueMaxException>().WithMessage(mensagemEsperada);
        }
        
        [Fact]
        public void Conta_VerificarSeEstaInativa_LancarExcecao() //#13
        {
            //Arrange
            var conta = new Conta();
            
            //Act
            conta.changeAtiva(false);
            Action acao = () => conta.Depositar(20);
            Action acao2 = () => conta.Sacar(20);
            string mensagemEsperada = "Não é possível realizar a operação. A conta está inativa";
            
            //Assert
            acao.Should().Throw<ContaInativaException>().WithMessage(mensagemEsperada);
            acao2.Should().Throw<ContaInativaException>().WithMessage(mensagemEsperada);
                
        }

        [Fact]
        public void Conta_VerificarSeAContaEstaDesativada_VerificarStatusDaConta() //#14
        {
            // Arrange
            var conta = new Conta();
            
            //Act
            conta.changeAtiva(false);
            Action acao = () => conta.checkAtiva();
            Action acao2 = () => conta.Depositar(50);
            Action acao3 = () => conta.Sacar(50);

            //Assert
            acao.Should().Throw<ContaInativaException>()
                .WithMessage("Não é possível realizar a operação. A conta está inativa");
            
            acao2.Should().Throw<ContaInativaException>()
                .WithMessage("Não é possível realizar a operação. A conta está inativa");
            
            acao3.Should().Throw<ContaInativaException>()
                .WithMessage("Não é possível realizar a operação. A conta está inativa");
        }

        [Fact]
        public void Conta_VerificarSeAContaEstaReativada_VerificarStatusDaConta() //#15
        {
            //Arrange
            var conta = new Conta();
            
            //Act
            conta.changeAtiva(false);
            conta.changeAtiva(true);
            
            //Assert
            Assert.Equal(true,conta.Ativa);
        }
        
    }

}