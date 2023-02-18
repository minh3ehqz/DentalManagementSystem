using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Utils
{
    public class ValidateBirthdayAttribute : ValidationAttribute
    {
        //kiểm tra năm sinh hợp lệ
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthday;
            var isDateValid = DateTime.TryParse(value.ToString(), out birthday);

            if (!isDateValid || birthday >= DateTime.Now)
            {
                return new ValidationResult("Thời gian sinh không hợp lệ.");
            }

            return ValidationResult.Success;
        }

    }
}
