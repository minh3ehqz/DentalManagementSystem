using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DentalManagementSystem.Utils
{
    public class NameWithoutDigitsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string name = value.ToString();
                if (Regex.IsMatch(name, @"\d"))
                {
                    return new ValidationResult("Tên không được chứa bất kỳ ký tự số nào.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
