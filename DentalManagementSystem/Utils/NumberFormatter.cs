using System.Globalization;

namespace DentalManagementSystem.Utils
{
	public class NumberFormatter
	{
		public static string Separator(double number)
		{
			return number.ToString("N0", new NumberFormatInfo()
			{
				NumberGroupSizes = new[] { 3 },
				NumberGroupSeparator = "."
			});
		}
	}
}
