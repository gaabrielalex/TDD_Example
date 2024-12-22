using Domain.Pessoa.Validator;
using Domain.Utils.Validators;
using FluentValidation;
using System;
using Utils.CPF;
using Utils.Phone;

namespace Domain.Pessoa
{
	public class PessoaModelValidator : AbstractValidator<PessoaModel>
	{
		public PessoaModelValidator(ICpfUtil cpfUtil, IPhoneUtil phoneUtil)
		{
			RuleFor(x => x.CPF)
			   .NotEmpty().WithMessage(GenericValidatorsErrorMessages.CampoObrigatorio)
			   .Must(cpf => cpfUtil.IsCpf(cpf)).WithMessage(GenericValidatorsErrorMessages.CampoInvalido);

			RuleFor(x => x.Celular)
				.NotEmpty().WithMessage(GenericValidatorsErrorMessages.CampoObrigatorio)
				.Must(phone => phoneUtil.IsPhoneNumber(phone)).WithMessage(GenericValidatorsErrorMessages.CampoInvalido);

			RuleFor(x => x.DataNascimento)
				.NotEmpty().WithMessage(GenericValidatorsErrorMessages.CampoObrigatorio)
				.LessThanOrEqualTo(DateTime.Now).WithMessage(PessoaModelErrorMessages.DataNascimentoDeveSerMaiorOuIgualDataAtual);
		}
	}
}
