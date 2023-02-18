using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Utils
{
    public class TwoWordsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
              if (value != null)
              {
                  string[] words = value.ToString().Trim().Split(' ');
                  if (words.Length < 2)
                  {
                      return new ValidationResult("Tên phải chứa ít nhất hai từ");
                  }
              }

              return ValidationResult.Success;
           
        }
    }
}