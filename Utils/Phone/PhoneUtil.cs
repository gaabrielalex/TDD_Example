using System.Text.RegularExpressions;

namespace Utils.Phone
{
	public class PhoneUtil : IPhoneUtil
	{
		public bool IsPhoneNumber(string phoneNumber)
		{
			if (string.IsNullOrEmpty(phoneNumber))
				return false;

			return Regex.Match(phoneNumber, @"^\+55[1-9][0-9]{1}[0-9]{8,9}$").Success;
		}
	}
}
