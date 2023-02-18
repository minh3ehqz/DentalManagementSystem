using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Utils
{
    public class ValidateBirthdayAttribute: ValidationAttribute
    {
        //kiểm tra năm sinh hợp lệ
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime birthday;
                if (DateTime.TryParse(value.ToString(), out birthday))
                {
                    if (birthday>=DateTime.Now)
                    {
                        return new ValidationResult(ErrorMessage ?? "Ngày sinh không hợp lệ.");
                    }
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "Ngày sinh không hợp lệ.");
        }

    }
}
