namespace Domain.Utils.Validators
{
	public class ValidationUtils
	{
		public static string FormatarMensagemDeErro(string errorMessage, string propertyName = null, int? minLength = null, int? maxLength = null, int? totalLength = null, int? exactLength = null)
		{
			if (!string.IsNullOrWhiteSpace(propertyName))
				errorMessage = errorMessage.Replace(FluentValidationPlaceholders.PropertyName, propertyName);
			if (minLength.HasValue)
				errorMessage = errorMessage.Replace(FluentValidationPlaceholders.MinLength.ToString(), minLength.Value.ToString());
			if (maxLength.HasValue)
				errorMessage = errorMessage.Replace(FluentValidationPlaceholders.MaxLength.ToString(), maxLength.Value.ToString());
			if (totalLength.HasValue)
				errorMessage = errorMessage.Replace(FluentValidationPlaceholders.TotalLength.ToString(), totalLength.Value.ToString());
			if (exactLength.HasValue)
				errorMessage = errorMessage.Replace(FluentValidationPlaceholders.ExactLength.ToString(), exactLength.Value.ToString());
			return errorMessage;
		}
	}
}
