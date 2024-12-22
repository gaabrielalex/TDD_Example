using Utils;
using Utils.Phone;

namespace Domain.Pessoa.Validator
{
	public class PessoaModelValidatorFactory
	{
		public static PessoaModelValidator Criar()
		{
			return new PessoaModelValidator(
				cpfUtil: new CpfUtil(),
				phoneUtil: new PhoneUtil()
			);
		}
	}
}
