using System.ComponentModel.DataAnnotations;

namespace DentalManagementSystem.Utils
{
    public class TwoWordsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string[] words = value.ToString().Split(' ');
                return words.Length >= 2;
            }
            return false;
        }
    }
}