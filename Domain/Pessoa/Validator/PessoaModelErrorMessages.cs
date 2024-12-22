namespace Domain.Pessoa.Validator
{
	public static class PessoaModelErrorMessages
	{
		public static string DataNascimentoDeveSerMaiorOuIgualDataAtual = $"O campo \"{nameof(PessoaModel.DataNascimento)}\" deve ser menor ou igual a hoje.";
	}
}
