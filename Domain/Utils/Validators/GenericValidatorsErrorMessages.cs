namespace Domain.Utils.Validators
{
	public static class GenericValidatorsErrorMessages
	{
		public static string CampoNaoAntingiuTamanhoMinimo = $"O campo \"{FluentValidationPlaceholders.PropertyName}\" não atingiu o tamanho mínimo de {FluentValidationPlaceholders.MinLength} caracter(es). Você informou um total de {FluentValidationPlaceholders.TotalLength} caracter(es).";
		public static string CampoUltrapassouTamanhoMaximo = $"O campo \"{FluentValidationPlaceholders.PropertyName}\" ultrapassou o tamanho máximo de {FluentValidationPlaceholders.MaxLength} caracter(es). Você informou um total de {FluentValidationPlaceholders.TotalLength} caracter(es).";
		public static string CampoObrigatorio = $"O campo \"{FluentValidationPlaceholders.PropertyName}\" é obrigatório.";
		public static string CampoInvalido = $"O campo \"{FluentValidationPlaceholders.PropertyName}\" é inválido.";
	}
}
