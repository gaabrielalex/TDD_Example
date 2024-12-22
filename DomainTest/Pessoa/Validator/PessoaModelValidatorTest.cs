using Domain.Pessoa.Validator;
using DomainTest.Pessoa.CenariosFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Domain.Pessoa;
using Domain.Utils.Validators;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System;

namespace DomainTest.Pessoa.Validator
{
	[TestClass]
	public class PessoaModelValidatorTest
	{
        [TestMethod]
		[TestCategory("IntegrationTest")]
		public void AoValidarPessoaValida_DevePassarNaValidacao()
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeTrue(because: "A pessoa é válida, logo a validação deve passar.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterStringsNulaEComEspacoEmBranco), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCpfNuloOuVazio_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string valor)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.CPF = valor;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: $"O campo \"{nameof(PessoaModel.CPF)}\" é obrigatório, não pode ser nulo nem vazio.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoObrigatorio,
				propertyName: nameof(pessoa.CPF)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");

		}

		[DataTestMethod]
		[TestCategory("IntegrationTest")]
		[DynamicData(nameof(ObterCpfsValidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCpfValido_DevePassarNaValidacao(string cpf)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.CPF = cpf;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeTrue(because: "O CPF assim como os demais campos da pessoa são válidos, logo a validação deve passar.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterCpfsValidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCpfValidoComMocks_DevePassarNaValidacao(string cpf)
		{
			var (validator, cpfUtil, phoneUtil) = PessoaModelValidatorTestCenariosFactory.ObterPessoaModelValidatorComMocks();
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();

			cpfUtil.Setup(x => x.IsCpf(It.IsAny<string>())).Returns(true);
			phoneUtil.Setup(x => x.IsPhoneNumber(It.IsAny<string>())).Returns(true);
			pessoa.CPF = cpf;

			var resultado = validator.Validate(pessoa);

			cpfUtil.Verify(x => x.IsCpf(It.IsAny<string>()), Times.Once);
			phoneUtil.Verify(x => x.IsPhoneNumber(It.IsAny<string>()), Times.Once);
			resultado.IsValid.Should().BeTrue(because: "O CPF assim como os demais campos da pessoa são válidos, logo a validação deve passar.");
		}

		[DataTestMethod]
		[TestCategory("IntegrationTest")]
		[DynamicData(nameof(ObterCpfsInvalidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCpfInvalido_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string cpf)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.CPF = cpf;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: "O CPF é inválido, logo a validação deve falhar.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoInvalido,
				propertyName: nameof(pessoa.CPF)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterCpfsInvalidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCpfInvalidoComMocks_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string cpf)
		{
			var (validator, cpfUtil, phoneUtil) = PessoaModelValidatorTestCenariosFactory.ObterPessoaModelValidatorComMocks();
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();

			cpfUtil.Setup(x => x.IsCpf(It.IsAny<string>())).Returns(false);
			phoneUtil.Setup(x => x.IsPhoneNumber(It.IsAny<string>())).Returns(true);
			pessoa.CPF = cpf;

			var resultado = validator.Validate(pessoa);

			cpfUtil.Verify(x => x.IsCpf(It.IsAny<string>()), Times.Once);
			phoneUtil.Verify(x => x.IsPhoneNumber(It.IsAny<string>()), Times.Once);
			resultado.IsValid.Should().BeFalse(because: "O CPF é inválido, logo a validação deve falhar.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoInvalido,
				propertyName: nameof(pessoa.CPF)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterStringsNulaEComEspacoEmBranco), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCelularNuloOuVazio_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string valor)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.Celular = valor;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: $"O campo \"{nameof(PessoaModel.Celular)}\" é obrigatório, não pode ser nulo nem vazio.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoObrigatorio,
				propertyName: nameof(pessoa.Celular)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[DataTestMethod]
		[TestCategory("IntegrationTest")]
		[DynamicData(nameof(ObterTelefonesValidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCelularValido_DevePassarNaValidacao(string celular)
		
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.Celular = celular;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeTrue(because: "O celular assim como os demais campos da pessoa são válidos, logo a validação deve passar.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterTelefonesValidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCelularValidoComMocks_DevePassarNaValidacao(string celular)
		{
			var (validator, cpfUtil, phoneUtil) = PessoaModelValidatorTestCenariosFactory.ObterPessoaModelValidatorComMocks();
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();

			cpfUtil.Setup(x => x.IsCpf(It.IsAny<string>())).Returns(true);
			phoneUtil.Setup(x => x.IsPhoneNumber(It.IsAny<string>())).Returns(true);
			pessoa.Celular = celular;

			var resultado = validator.Validate(pessoa);

			cpfUtil.Verify(x => x.IsCpf(It.IsAny<string>()), Times.Once);
			phoneUtil.Verify(x => x.IsPhoneNumber(It.IsAny<string>()), Times.Once);
			resultado.IsValid.Should().BeTrue(because: "O celular assim como os demais campos da pessoa são válidos, logo a validação deve passar.");
		}

		[DataTestMethod]
		[TestCategory("IntegrationTest")]
		[DynamicData(nameof(ObterTelefonesInvalidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCelularInvalido_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string celular)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.Celular = celular;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: "O celular é inválido, logo a validação deve falhar.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
							errorMessage: GenericValidatorsErrorMessages.CampoInvalido,
											propertyName: nameof(pessoa.Celular)
														), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterTelefonesInvalidos), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComCelularInvalidoComMocks_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(string celular)
		{
			var (validator, cpfUtil, phoneUtil) = PessoaModelValidatorTestCenariosFactory.ObterPessoaModelValidatorComMocks();
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();

			cpfUtil.Setup(x => x.IsCpf(It.IsAny<string>())).Returns(true);
			phoneUtil.Setup(x => x.IsPhoneNumber(It.IsAny<string>())).Returns(false);
			pessoa.Celular = celular;

			var resultado = validator.Validate(pessoa);

			cpfUtil.Verify(x => x.IsCpf(It.IsAny<string>()), Times.Once);
			phoneUtil.Verify(x => x.IsPhoneNumber(It.IsAny<string>()), Times.Once);
			resultado.IsValid.Should().BeFalse(because: "O celular é inválido, logo a validação deve falhar.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoInvalido,
				propertyName: nameof(pessoa.Celular)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[TestMethod]
		public void AoValidarPessoaComDataNascimentoNula_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente()
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.DataNascimento = default;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: $"O campo \"{nameof(PessoaModel.DataNascimento)}\" é obrigatório, não pode ser nulo.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: GenericValidatorsErrorMessages.CampoObrigatorio,
				propertyName: "Data Nascimento"
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		[DataTestMethod]
		[DynamicData(nameof(ObterDatasDeNascimentoValidas), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComDataNascimentoValida_DevePassarNaValidacao(DateTime dataNascimento)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.DataNascimento = dataNascimento;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeTrue(because: "A data de nascimento assim como os demais campos da pessoa são válidos, logo a validação deve passar.");
		}


		[DataTestMethod]
		[DynamicData(nameof(ObterDatasDeNascimentoInvalidas), DynamicDataSourceType.Method)]
		public void AoValidarPessoaComDataNascimentoMaiorQueDataAtual_DeveFalharNaValidacaoContendoMensagemDeErroCorrespondente(DateTime dataNascimento)
		{
			var pessoa = PessoaModelValidatorTestCenariosFactory.ObterPessoaValida();
			pessoa.DataNascimento = dataNascimento;

			var validator = PessoaModelValidatorFactory.Criar();
			var resultado = validator.Validate(pessoa);

			resultado.IsValid.Should().BeFalse(because: $"O campo \"{nameof(PessoaModel.DataNascimento)}\" não pode ser maior que a data atual.");
			var erros = resultado.Errors.Select(x => x.ErrorMessage).ToList();
			erros.Should().Contain(ValidationUtils.FormatarMensagemDeErro(
				errorMessage: PessoaModelErrorMessages.DataNascimentoDeveSerMaiorOuIgualDataAtual,
				propertyName: nameof(pessoa.DataNascimento)
			), because: "A mensagem de erro da validação deve ser retornada corretamente.");
		}

		public static IEnumerable<object[]> ObterCpfsValidos()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterCpfsValidos();
		}

		public static IEnumerable<object[]> ObterCpfsInvalidos()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterCpfsInvalidos();
		}

		public static IEnumerable<object[]> ObterTelefonesValidos()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterTelefonesValidos();
		}

		public static IEnumerable<object[]> ObterTelefonesInvalidos()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterTelefonesInvalidos();
		}

		public static IEnumerable<object[]> ObterDatasDeNascimentoValidas()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterDatasDeNascimentoValidas();
		}

		public static IEnumerable<object[]> ObterDatasDeNascimentoInvalidas()
		{
			return PessoaModelValidatorTestCenariosFactory.ObterDatasDeNascimentoInvalidas();
		}

		public static IEnumerable<object[]> ObterStringsNulaEComEspacoEmBranco()
		{
			return new List<object[]>
			{
				new object[] { null },
				new object[] { string.Empty },
				new object[] { " " },
				new object[] { "  " },
				new object[] { "   " },
				new object[] { "    " },
			};
		}

	}
}
