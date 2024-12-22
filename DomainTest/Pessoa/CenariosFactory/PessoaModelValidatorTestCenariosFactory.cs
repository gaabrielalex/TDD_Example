using Domain.Pessoa;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using Utils.CPF;
using Utils.Phone;

namespace DomainTest.Pessoa.CenariosFactory
{
	public class PessoaModelValidatorTestCenariosFactory
	{
		public static PessoaModel ObterPessoaValida()
		{
			return new PessoaModel
			{
				Nome = "Nome",
				CPF = "08334011148",
				Celular = "+5582988817244",
				DataNascimento = DateTime.Now.AddYears(-20)
			};
		}

		public static (PessoaModelValidator, Mock<ICpfUtil>, Mock<IPhoneUtil>) ObterPessoaModelValidatorComMocks()
		{
			var cpfUtilMockado = new Mock<ICpfUtil>();
			var phoneUtil = new Mock<IPhoneUtil>();

			return (
				new PessoaModelValidator(
					cpfUtil: cpfUtilMockado.Object,
					phoneUtil: phoneUtil.Object
				),
				cpfUtilMockado,
				phoneUtil
			);
		}

		public static IEnumerable<object[]> ObterCpfsValidos()
		{
			yield return new object[] { "08334011148" };
			yield return new object[] { "03805615060" };
			yield return new object[] { "00371898080" };
			yield return new object[] { "25067722002" };
			yield return new object[] { "70823651010" };
			yield return new object[] { "33494947066" };
		}

		public static IEnumerable<object[]> ObterCpfsInvalidos()
		{
			yield return new object[] { "08334011147" };
			yield return new object[] { "03805615061" };
			yield return new object[] { "00371898081" };
			yield return new object[] { "25067722003" };
			yield return new object[] { "70823651011" };
			yield return new object[] { "33494947067" };
		}

		public static IEnumerable<object[]> ObterTelefonesValidos()
		{
			yield return new object[] { "+5582988817244" };
			yield return new object[] { "+5554929112568" };
			yield return new object[] { "+5579935365781" };
			yield return new object[] { "+5586932073888" };
			yield return new object[] { "+5582931145194" };
			yield return new object[] { "+5548932171588" };
			yield return new object[] { "+5562937811284" };
			yield return new object[] { "+5584924589701" };
		}

		public static IEnumerable<object[]> ObterTelefonesInvalidos()
		{
			yield return new object[] { "12345" };         
			yield return new object[] { "abcdefghij" };    
			yield return new object[] { "123-abc-7890" };  
			yield return new object[] { "(12) 3456" };     
			yield return new object[] { "12345678901234" };
			yield return new object[] { "" };              
			yield return new object[] { "        " };                
			yield return new object[] { "+55 12345-678" }; 
			yield return new object[] { "(99)1234 567" };  
		}

		public static IEnumerable<object[]> ObterDatasDeNascimentoValidas()
		{
			yield return new object[] { DateTime.Now.AddYears(-20) };
			yield return new object[] { DateTime.Now.AddYears(-30) };
			yield return new object[] { DateTime.Now.AddYears(-40) };
			yield return new object[] { DateTime.Now.AddYears(-50) };
			yield return new object[] { DateTime.Now.AddYears(-60) };
			yield return new object[] { DateTime.Now.AddYears(-70) };
		}

		public static IEnumerable<object[]> ObterDatasDeNascimentoInvalidas()
		{
			yield return new object[] { DateTime.Now.AddYears(20) };
			yield return new object[] { DateTime.Now.AddYears(30) };
			yield return new object[] { DateTime.Now.AddYears(40) };
			yield return new object[] { DateTime.Now.AddYears(50) };
			yield return new object[] { DateTime.Now.AddYears(60) };
			yield return new object[] { DateTime.Now.AddYears(70) };
		}

	}
}
