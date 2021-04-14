using FluentAssertions;
using System;
using System.Threading;
using Vaquinha.Automated.UI.Tests;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
    public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
    {
        private DSL _dsl;

		private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
			_dsl = new DSL("https://localhost:44317/");

		}

		public void Dispose()
		{
			_dsl.Dispose();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
            _dsl.NavegarHome();

            // Act
            var logoVaquinha = _dsl.ObterElementoPorClasse("vaquinha-logo");

			// Assert
			logoVaquinha.Displayed.Should().BeTrue(because:"logo exibido");
		}

		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
            _dsl.NavegarHome();

			//Act
            _dsl.ClicarBotaoPorClasse("btn-yellow");
            _dsl.EscreverPorId("valor", doacao.Valor.ToString());
            _dsl.EscreverPorId("DadosPessoais_Nome", doacao.DadosPessoais.Nome);
            _dsl.EscreverPorId("EnderecoCobranca_TextoEndereco", doacao.EnderecoCobranca.TextoEndereco);
            _dsl.EscreverPorId("EnderecoCobranca_Numero", doacao.EnderecoCobranca.Numero);
            _dsl.EscreverPorId("DadosPessoais_Email", doacao.DadosPessoais.Email);
            _dsl.EscreverPorId("EnderecoCobranca_Cidade", doacao.EnderecoCobranca.Cidade);
            _dsl.EscreverPorId("estado", doacao.EnderecoCobranca.Estado);
            _dsl.EscreverPorId("DadosPessoais_MensagemApoio", "Teste de UI");
            _dsl.EscreverPorId("cep", doacao.EnderecoCobranca.CEP);
            _dsl.EscreverPorId("EnderecoCobranca_Complemento", doacao.EnderecoCobranca.Complemento);
            _dsl.EscreverPorId("telefone", doacao.EnderecoCobranca.Telefone);
            _dsl.EscreverPorId("FormaPagamento_NomeTitular", doacao.FormaPagamento.NomeTitular);
            _dsl.EscreverPorId("cardNumber", "5516-7162-3355-2410"); //numero obtido em gerador online
            _dsl.EscreverPorId("validade", doacao.FormaPagamento.Validade);
            _dsl.EscreverPorId("cvv", doacao.FormaPagamento.CVV);
            _dsl.ClicarBotaoPorClasse("btn-yellow");
            Thread.Sleep(2000);

            //Assert
            _dsl.PaginaContemTexto("Doação realizada com sucesso").Should().Be(true);
		}
	}
}